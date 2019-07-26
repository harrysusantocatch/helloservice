using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.Entities.Request
{
    public class SendMessageRequest
    {
        public string ChatRoomID { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public List<object> Contents { get; set;}
    }
}
