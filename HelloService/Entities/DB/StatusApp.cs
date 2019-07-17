using Catcher.DB.DTO;
using static HelloService.Helper.Constant;

namespace HelloService.Entities.DB
{
    [DtoClass(DATABASE_NAME, "STATUS_APPLICATION")]
    public class StatusApp : Dto
    { 
        public string Status { get; set; }
    }
}
