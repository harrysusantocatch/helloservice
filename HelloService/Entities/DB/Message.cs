using System;
using System.Collections.Generic;
using Catcher.DB.DTO;
using MongoDB.Bson.Serialization.Attributes;
using static HelloService.Helper.Constant;

namespace HelloService.Entities.DB
{
    [DtoClass(DATABASE_NAME, "MESSAGES")]
    public class Message : Dto
    {
        [DtoUniqueField]
        public ChatRoom ChatRoom { get; set; }

        [BsonSerializer(typeof(ObjectRef<Message>))]
        public Message ChildMessage { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool Read { get; set; }
        public MessageType Type { get; set; }
        public List<object> Contents { get; set; }
        public bool DeletedFromSender { get; set; }
        public bool DeletedFromReceiver { get; set; }
    }

    public enum MessageType
    {
        TEXT, IMAGE, AUDIO, VIDEO, LOCATION
    }
}
