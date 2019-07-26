using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.Entities.Request
{
    public class ChatRoomRequest
    {
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
    }
}
