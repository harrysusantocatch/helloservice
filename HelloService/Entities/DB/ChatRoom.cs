using Catcher.DB.DTO;
using MongoDB.Bson.Serialization.Attributes;
using static HelloService.Helper.Constant;

namespace HelloService.Entities.DB
{
    [DtoClass(DATABASE_NAME, "CHATROOM")]
    public class ChatRoom : Dto
    {

        [DtoCompositeField, BsonSerializer(typeof(ObjectRef<User>))]
        public User Sender { get; set; }

        [DtoCompositeField, BsonSerializer(typeof(ObjectRef<User>))]
        public User Receiver { get; set; }

        [BsonSerializer(typeof(ObjectRef<Message>))]
        public Message LastMessage { get; set; }
    }
}
