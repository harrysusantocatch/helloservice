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

        public ChatRoomResponse(User user, ChatRoom chatRoom, string gmt)
        {
            var displayUser = user.Phone == chatRoom.Sender.Phone ? chatRoom.Receiver : chatRoom.Sender;
            Name = displayUser.FullName;
            ProfilePicture = displayUser.ProfilePicture;
            Text = chatRoom.LastMessage.Text;
            Read = chatRoom.LastMessage.Read;
            var clientDateNow = TimeConverter.ConvertFromServerTime(Constant.SERVER_TIME, gmt);
            var lastClientDate = TimeConverter.ConvertFromServerTime(chatRoom.LastMessage.Date, gmt);
            var days = (clientDateNow - lastClientDate).Days;
            if (days == 0)
            {
                Date = lastClientDate.ToString("HH:mm");
            }
            else if (days == 1) Date = "Yesterday";
            else Date = lastClientDate.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
