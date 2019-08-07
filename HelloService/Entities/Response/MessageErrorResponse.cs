using System;
namespace HelloService.Entities.Response
{
    public class MessageErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        public MessageErrorResponse(int code, string message)
        {
            ErrorCode = code;
            Message = message;
        }
    }
}
