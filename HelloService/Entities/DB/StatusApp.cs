using Catcher.DB.DTO;

namespace HelloService.Entities.DB
{
    [DtoClass("CATCH_DB", "STATUS_APPLICATION")]
    public class StatusApp : Dto
    { 
        public string Status { get; set; }
    }
}
