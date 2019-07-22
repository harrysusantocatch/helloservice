using System;
using HelloService.Entities.Model;

namespace HelloService.Entities.Request
{
    public class UpdateProfilePictureRequest
    {
        public Blob Content { get; set; }
    }
}
