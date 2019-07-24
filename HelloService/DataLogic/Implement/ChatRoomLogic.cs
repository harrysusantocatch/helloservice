using Catcher.DB.DTO;
using HelloService.DataAccess.Implement;
using HelloService.DataLogic.Interface;
using HelloService.Entities.DB;
using HelloService.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.DataLogic.Implement
{
    public class ChatRoomLogic : IChatRoomLogic
    {
        private readonly ChatRoomDao chatRoomDao;
        public ChatRoomLogic(ChatRoomDao _chatRoomDao)
        {
            chatRoomDao = _chatRoomDao;
        }

        public List<ChatRoomResponse> GetChatRooms(User user)
        {
            var result = new List<ChatRoomResponse>();
            var userRef = user.ToRef();
            var chatRooms = chatRoomDao.GetChatRooms(userRef);
            if(chatRooms.Count > 0)
            {
                var cleanChatRoom = CleanChatRoom(chatRooms);
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
                foreach(var chatRoom in chatRooms)
                {
                    result.Add(new ChatRoomResponse(chatRoom));
                }
            }
            return result;
        }

        private List<ChatRoom> CleanChatRoom(IList<ChatRoom> chatRooms)
        {
            foreach(var chatRoom in chatRooms)
            {
                if (chatRoom.LastMessage == null) chatRooms.Remove(chatRoom);
            }
            return chatRooms.ToList();
        }
    }
}
