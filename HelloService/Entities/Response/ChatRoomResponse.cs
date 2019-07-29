using Catcher.DB.DTO;
using HelloService.Entities.DB;
using HelloService.Entities.Model;
using HelloService.Helper;
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
        public string Date { get; set; }

        public ChatRoomResponse(User user, ChatRoom chatRoom, Message message, string gmt)
        {
            var displayUser = user.Phone == chatRoom.User1.Phone ? chatRoom.User2 : chatRoom.User1;
            Name = displayUser.FullName;
            ProfilePicture = displayUser.ProfilePicture;
            Text = message.Text;
            Read = message.Read;
            Date = TimeConverter.GetDisplayDate(new DateTime(message.Date), gmt);
        }

    }
}
