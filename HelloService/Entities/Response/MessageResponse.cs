using HelloService.Entities.DB;
using HelloService.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.Entities.Response
{
    public class MessageResponse
    {
        private Message message;

        public MessageResponse(Message message)
        {
            this.message = message;
        }

        public string MessageID { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public List<object> Contents { get; set; }
        public bool Read { get; set; }
        public string Name { get; set; }
        public long Date { get; set; }
        public Blob ProfilePicture { get; set; }
    }
}
