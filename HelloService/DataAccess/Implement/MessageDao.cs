using Catcher.DB.DAO;
using HelloService.DataAccess.Interface;
using HelloService.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.DataAccess.Implement
{
    public class MessageDao : BaseDao<Message>, IMessageDao
    {
        private static readonly IDao<Message> messageDao = DaoContext.GetDao<Message>();

        public List<Message> FindMessageByLastDate(string chatRoomID, long date)
        {
            throw new NotImplementedException();
        }
    }
}
