using Catcher.DB.DTO;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using static HelloService.Helper.Constant;

namespace HelloService.Entities.DB
{
    [DtoClass(DATABASE_NAME, "CHATROOM")]
    public class ChatRoom : Dto
    {

        [DtoCompositeField, BsonSerializer(typeof(ObjectRef<User>))]
        public User User1 { get; set; }

        [DtoCompositeField, BsonSerializer(typeof(ObjectRef<User>))]
        public User User2 { get; set; }

        public List<MongoDBRef> MessagesRef { get; set; }
    }
}
