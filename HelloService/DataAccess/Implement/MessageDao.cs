using Catcher.DB.DAO;
using Catcher.DB.DTO;
using Catcher.Model;
using HelloService.DataAccess.Interface;
using HelloService.Entities.DB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.DataAccess.Implement
{
    public class MessageDao : BaseDao<Message>, IMessageDao
    {
        private static readonly IDao<Message> messageDao = DaoContext.GetDao<Message>();

        public List<Message> FindMessageByLastDate(ChatRoom chatRoom, long longLastDate)
        {
            var builder = new FilterDefinitionBuilder<Message>();
            var filterChat = builder.Eq("ChatRoom", chatRoom.ToRef());
            var filterDate = builder.Gt("Date", longLastDate);
            var messages = messageDao.Find.WhenMatch(filterChat & filterDate, new PageDefinition("Date", false));
            return new List<Message>(messages);
        }

        internal List<Message> FindMessageByChatRoom(ChatRoom chatRoom)
        {
            var builder = new FilterDefinitionBuilder<Message>();
            var filterChat = builder.Eq("ChatRoom", chatRoom.ToRef());
            var messages = messageDao.Find.WhenMatch(filterChat, new PageDefinition("Date", false));
            return new List<Message>(messages);
        }
    }
}
