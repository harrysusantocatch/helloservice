using Catcher.DB.DAO;
using Catcher.DB.DTO;
using HelloService.DataAccess.Interface;
using HelloService.Entities.DB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.DataAccess.Implement
{
    public class ChatRoomDao : BaseDao<ChatRoom>, IChatRoomDao
    {
        private static readonly IDao<ChatRoom> chatRoomDao = DaoContext.GetDao<ChatRoom>();

        public IList<ChatRoom> GetMyChatRoom(MongoDBRef userRef)
        {
            var builder = new FilterDefinitionBuilder<ChatRoom>();
            var filterSender = builder.Eq("User1", userRef);
            var filterReceiver = builder.Eq("User2", userRef);
            var list = chatRoomDao.Find.WhenMatch(filterSender | filterReceiver);
            return list;
        }

        public ChatRoom FindChatRoomByUsers(User sender, User receiver)
        {
            var compositeID = new Dictionary<string, object>
            {
                { "User1", sender.ToRef() },
                { "User2", receiver.ToRef() }
            };
            var chatRoom = chatRoomDao.Find.ByCompositeID(compositeID);
            return chatRoom;
        }
    }
}
