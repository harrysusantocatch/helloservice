using HelloService.Entities.DB;
using HelloService.Entities.Response;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.DataAccess.Interface
{
    interface IChatRoomDao
    {
        IList<ChatRoom> GetChatRooms(MongoDBRef userRef);
    }
}
