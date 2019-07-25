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
        public ChatRoomLogic()
        {
            chatRoomDao = new ChatRoomDao();
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
