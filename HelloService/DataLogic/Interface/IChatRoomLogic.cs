using HelloService.Entities.DB;
using HelloService.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.DataLogic.Interface
{
    public interface IChatRoomLogic
    {
        List<ChatRoomResponse> GetChatRooms(User user, string gmt);
    }
}
