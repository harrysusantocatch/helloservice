using Catcher.DB.DTO;
using HelloService.DataAccess.Implement;
using HelloService.DataLogic.Interface;
using HelloService.Entities.DB;
using HelloService.Entities.Request;
using HelloService.Entities.Response;
using HelloService.Helper;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloService.DataLogic.Implement
{
    public class ChatLogic : IChatRoomLogic
    {
        private static ChatLogic _instance;
        public static ChatLogic Instance => _instance ?? (_instance = new ChatLogic());
        private readonly ChatRoomDao chatRoomDao;
        private readonly UserDao userDao;
        private readonly MessageDao messageDao;
        private readonly LastSeenDao lastSeenDao;

        public ChatLogic()
        {
            chatRoomDao = new ChatRoomDao();
            userDao = new UserDao();
            messageDao = new MessageDao();
            lastSeenDao = new LastSeenDao();
        }

        public List<ChatRoomResponse> GetChatRooms(User user, string gmt)
        {
            var result = new List<ChatRoomResponse>();
            var userRef = user.ToRef();
            var chatRooms = chatRoomDao.GetMyChatRoom(userRef);
            if(chatRooms.Count > 0)
            {
                foreach(var chatRoom in chatRooms)
                {
                    if (chatRoom.MessagesRef == null) continue;
                    int lastIndex = chatRoom.MessagesRef.Count - 1;
                    var message = GetLastMessage(user, chatRoom, lastIndex);
                    if(message != null)
                    {
                        result.Add(new ChatRoomResponse(user, chatRoom, message, gmt));
                    }
                }
            }
            return result;
        }

        private Message GetLastMessage(User user, ChatRoom chatRoom, int lastIndex)
        {
            var message = chatRoom.MessagesRef[lastIndex].Retrieve<Message>();
            if (user == message.MessageOwner)
            {
                if (!message.DeletedFromSender) return message;
            }
            else
            {
                if (!message.DeletedFromReceiver) return message;
            }
            return null;
        }

        public bool SendMessage(User user, SendMessageRequest request)
        {
            var chatRoom = chatRoomDao.FindByID(request.ChatRoomID);
            if (chatRoom == null) return false;
            var invalidUser = user != chatRoom.User1 && user != chatRoom.User2;
            if (invalidUser) return false;
            var model = new Message
            {
                ChatRoom = chatRoom,
                Date = Constant.SERVER_TIME.Ticks,
                MessageOwner = user,
                Read = false,
                Text = request.Text,
                Type = (MessageType)request.Type,
                Contents = request.Contents
            };
            var message = messageDao.InsertAndGet(model);
            if (message != null)
            {
                if(chatRoom.MessagesRef == null) chatRoom.MessagesRef = new List<MongoDBRef>();
                chatRoom.MessagesRef.Add(message.ToRef());
                chatRoomDao.Update(chatRoom, new string[] { "MessagesRef" });

                // TODO send notif
                var receiver = user == chatRoom.User1 ? chatRoom.User2 : chatRoom.User1;
                return true;
            }else return false;
        }

        public void InvokeStatus(User user, string statusOrStrDate)
        {
            var lastSeen = lastSeenDao.FindByUniqueID("User", user.ToRef());
            if (lastSeen == null)
            {
                var model = new LastSeen
                {
                    User = user,
                    LongDateString = statusOrStrDate
                };
                lastSeenDao.Insert(model);
            }
            else
            {
                lastSeen.LongDateString = statusOrStrDate;
                lastSeenDao.Update(lastSeen, new string[] { "LongDateString" });
            }
        }

        public bool RemoveMessageForMe(User user, string messageID)
        {
            var message = messageDao.FindByID(messageID);
            if (message == null) return false;
            var chatRoom = message.ChatRoom;
            var invalidUser = user != chatRoom.User1 && user != chatRoom.User2;
            if (invalidUser) return false;
            return DeleteMessageByUser(user, message);
        }

        private bool DeleteMessageByUser(User user, Message message)
        {
            if (user == message.MessageOwner)
            {
                message.DeletedFromSender = true;
                return messageDao.Update(message, new string[] { "DeletedFromSender" });
            }
            else
            {
                message.DeletedFromReceiver = true;
                return messageDao.Update(message, new string[] { "DeletedFromReceiver" });
            }
        }

        public bool RemoveChatRoom(User user, string chatRoomID)
        {
            var chatRoom = chatRoomDao.FindByID(chatRoomID);
            if (chatRoom == null) return false;
            var messages = messageDao.FindMessageByChatRoom(chatRoom);
            Task.Run(()=> RemoveMessage(user, messages));
            return true;
        }

        private void RemoveMessage(User user, List<Message> messages)
        {
            if(messages.Count > 0)
            {
                foreach(var message in messages)
                {
                    DeleteMessageByUser(user, message);
                }
            }
        }

        public bool RemoveMessageForAll(User user, string messageID)
        {
            var message = messageDao.FindByID(messageID);
            if (message == null) return false;
            var chatRoom = message.ChatRoom;
            var invalidUser = user != chatRoom.User1 && user != chatRoom.User2;
            if (invalidUser) return false;
            message.DeletedFromSender = true;
            message.DeletedFromReceiver = true;
            return messageDao.Update(message, new string[] { "DeletedFromSender", "DeletedFromReceiver" });
        }

        public LastSeenResponse GetLastSeenByPhone(string phone, string gmt)
        {
            var user = userDao.FindByPhoneNumber(phone);
            if (user == null) return null;
            var lastSeen = lastSeenDao.FindByUniqueID("User", user.ToRef());
            if (lastSeen == null) return null;
            return new LastSeenResponse(lastSeen.LongDateString, gmt);
        }

        public List<MessageResponse> GetMessage(User user, string chatRoomID, string longStrDate)
        {
            var responses = new List<MessageResponse>();
            var chatRoom = chatRoomDao.FindByID(chatRoomID);
            if (chatRoom == null) return responses;
            var invalidUser = user != chatRoom.User1 && user != chatRoom.User2;
            if (invalidUser) return responses;
            long longDate = longStrDate == null ? Constant.DEFAULT_LASTDATE : long.Parse(longStrDate);
            var messages = messageDao.FindMessageByLastDate(chatRoom, longDate);
            if (messages.Count > 0)
            {
                foreach (var message in messages)
                {
                    if(user == message.MessageOwner)
                    {
                        if(!message.DeletedFromSender) responses.Add(new MessageResponse(message));
                    }
                    else
                    {
                        if(!message.DeletedFromReceiver) responses.Add(new MessageResponse(message));
                    }
                    
                }
            }

            Task.Run(() => UpdateReads(user, messages));
            return responses;
        }

        private void UpdateReads(User user, List<Message> messages)
        {
            foreach (var message in messages)
            {
                if(message.MessageOwner != user)
                {
                    message.Read = true;
                    messageDao.Update(message, new string[] { "Read" });
                }
            }
        }

        public object CreatChatRoom(User user, ChatRoomRequest request)
        {
            var receiver = userDao.FindByID(request.ReceiverID);
            if (receiver == null) return new MessageErrorResponse(106, "User tidak terdaftar");
            var chatRoom = chatRoomDao.FindChatRoomByUsers(user, receiver);
            if (chatRoom == null)
            {
                chatRoom = chatRoomDao.FindChatRoomByUsers(receiver, user);
                if(chatRoom == null)
                {
                    var model = new ChatRoom
                    {
                        User2 = receiver,
                        User1 = user
                    };
                    chatRoom = chatRoomDao.InsertAndGet(model);
                }
            }
            return new { ChatID = chatRoom.ID.ToString() };
        }
    }
}
