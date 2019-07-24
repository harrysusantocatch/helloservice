using HelloService.Entities.DB;
using HelloService.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.Entities.Response
{
    public class ChatRoomResponse
    {
        public string Name { get; set; }
        public Blob ProfilePicture { get; set; }
        public string Text { get; set; }
        public bool Read { get; set; }
        private string _date;

        public ChatRoomResponse(ChatRoom chatRoom)
        {
            // TODO
        }

        public string Date {
            get
            {
                if(_date != null)
                {
                    //format date 9:30 12:17 19:20 yesterday 31/10/2019
                }
                return null;
            }
            set
            {
                _date = value;
            } }

    }
}
