using System;
namespace HelloService.Entities.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Expire { get; set; }
    }
}
