using System;
namespace HelloService.Entities.Request
{
    public class ValidationCodeRequest
    {
        public string Code { get; set; }
        public string Phone { get; set; }
    }
}
