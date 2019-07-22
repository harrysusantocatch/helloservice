using HelloService.Entities.Model;

namespace HelloService.Entities.Request
{
    public class RegisterRequest
    {
        public string Phone { get; set; }
        public Device Device { get; set; }
    }
}
