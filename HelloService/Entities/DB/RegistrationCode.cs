using System;
using Catcher.DB.DTO;
using MongoDB.Bson.Serialization.Attributes;
using static HelloService.Helper.Constant;

namespace HelloService.Entities.DB
{
    [DtoClass(DATABASE_NAME, "REGISTRATION_CODE")]
    public class RegistrationCode :Dto
    {
        [DtoUniqueField]
        public string Phone { get; set; }
        public string Code { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
