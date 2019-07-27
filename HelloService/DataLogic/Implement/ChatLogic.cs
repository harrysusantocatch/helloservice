using Catcher.DB.DTO;
using HelloService.DataAccess.Implement;
using HelloService.DataLogic.Interface;
using HelloService.Entities.DB;
using HelloService.Entities.Request;
using HelloService.Entities.Response;
using HelloService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.DataLogic.Implement
{
    public class ChatLogic : IChatRoomLogic
    {
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
            var chatRooms = chatRoomDao.GetChatRooms(userRef);
            if(chatRooms.Count > 0)
            {
                var cleanChatRoom = CleanChatRoom(chatRooms);
                if (cleanChatRoom.Count == 0) return result;
                cleanChatRoom.Sort((chat1, chat2) =>
                {
                    if (chat1.LastMessage.Date > chat2.LastMessage.Date)
                    {
                        return -1;
                    }
                    else if (chat1.LastMessage.Date > chat2.LastMessage.Date)
                    {
                        return 1;
                    }
                    else
                        return 0;
                });
                foreach(var chatRoom in cleanChatRoom)
                {
                    result.Add(new ChatRoomResponse(user, chatRoom, gmt));
                }
            }
            return result;
        }

        public bool SendMessage(User user, SendMessageRequest request)
        {
            var chatRoom = chatRoomDao.FindByID(request.ChatRoomID);
            if (chatRoom == null) return false;
            var invalidUser = user != chatRoom.Sender && user != chatRoom.Receiver;
            if (invalidUser) return false;
            var model = new Message
            {
                ChatRoom = chatRoom,
                Date = Constant.SERVER_TIME,
                MessageOwner = user,
                Read = false,
                Text = request.Text,
                Type = (MessageType)request.Type,
                Contents = request.Contents
            };
            var message = messageDao.InsertAndGet(model);
            if (message != null)
            {
                chatRoom.LastMessage = message;
                chatRoomDao.Update(chatRoom, new string[] { "LastMessage" });

                // TODO send notif
                var receiver = user == chatRoom.Sender ? chatRoom.Receiver : chatRoom.Sender;
                return true;
            }else return false;
        }

        public bool InvokeStatus(User user, string statusOrStrDate)
        {
            var lastSeen = lastSeenDao.FindByUniqueID("User", user.ToRef());
            if (lastSeen == null)
            {
                var model = new LastSeen
                {
                    User = user,
                    LongDateString = statusOrStrDate
                };
                return lastSeenDao.Insert(model);
            }
            else
            {
                lastSeen.LongDateString = statusOrStrDate;
                return lastSeenDao.Update(lastSeen, new string[] { "LongDateString" });
            }
        }

        public LastSeenResponse GetLastSeenByPhone(string phone, string gmt)
        {
            var user = userDao.FindByPhoneNumber(phone);
            if (user == null) return null;
            var lastSeen = lastSeenDao.FindByUniqueID("User", user.ToRef());
            if (lastSeen == null) return null;
            return new LastSeenResponse(lastSeen.LongDateString, gmt);
        }

        public List<MessageResponse> GetMessage(User user, string chatRoomID, string lastDate)
        {
            var responses = new List<MessageResponse>();
            var chatRoom = chatRoomDao.FindByID(chatRoomID);
            if (chatRoom == null) return responses;
            var invalidUser = user != chatRoom.Sender && user != chatRoom.Receiver;
            if (invalidUser) return responses;
            long date = long.Parse(lastDate);
            var messages = messageDao.FindMessageByLastDate(chatRoomID, date);
            if (messages.Count > 0)
            {
                foreach (var message in messages)
                {
                    responses.Add(new MessageResponse(message));
                }
            }

            // TODO nanti bikin update Read di controller atau async
            AsyUpdateReads(messages);
            return responses;
        }

        private void AsyUpdateReads(List<Message> messages)
        {
            foreach (var message in messages)
            {
                message.Read = true;
                messageDao.Update(message, new string[] { "Read" });
            }
        }

        public bool CreatChatRoom(ChatRoomRequest request)
        {
            var sender = userDao.FindByID(request.SenderID);
            if (sender == null) return false;
            var receiver = userDao.FindByID(request.ReceiverID);
            if (receiver == null) return false;
            var model = new ChatRoom
            {
                Receiver = receiver,
                Sender = sender
            };
            var success = chatRoomDao.Insert(model);
            return success;
        }

        private List<ChatRoom> CleanChatRoom(IList<ChatRoom> chatRooms)
        {
            foreach(var chatRoom in chatRooms)
            {
                if (chatRoom.LastMessage == null) chatRooms.Remove(chatRoom);
            }
            return new List<ChatRoom>(chatRooms);
        }
    }
}
