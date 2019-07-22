using Catcher.DB.DTO;
using HelloService.Entities.Model;
using static HelloService.Helper.Constant;

namespace HelloService.Entities.DB
{
    [DtoClass(DATABASE_NAME, "USER")]
    public class User : Dto
    {
        [DtoUniqueField]
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string About { get; set; }

        [DtoUniqueField]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SecurityCode { get; set; }

        public Device Device { get; set; }
        public Blob ProfilePicture { get; set; }

        public bool Active { get; set; }
    }
}
