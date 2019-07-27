using Catcher.DB.DTO;
using MongoDB.Bson.Serialization.Attributes;
using static HelloService.Helper.Constant;

namespace HelloService.Entities.DB
{
    [DtoClass(DATABASE_NAME, "CHATROOM")]
    public class LastSeen : Dto
    {
        [DtoUniqueField, BsonSerializer(typeof(ObjectRef<User>))]
        public User User { get; set; }
        public string LongDateString { get; set; } // long date string or status 'Online'
    }
}
