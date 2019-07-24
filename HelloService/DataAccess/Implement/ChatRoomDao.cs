using Catcher.DB.DAO;
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

        public IList<ChatRoom> GetChatRooms(MongoDBRef userRef)
        {
            var builder = new FilterDefinitionBuilder<ChatRoom>();
            var filterSender = builder.Eq("Sender", userRef);
            var filterReceiver = builder.Eq("Receiver", userRef);
            var list = chatRoomDao.Find.WhenMatch(filterSender & filterReceiver);
            return list;
        }


    }
}
