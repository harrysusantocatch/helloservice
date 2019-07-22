using System;
namespace HelloService.Entities.Model
{
    public class Device
    {
        public DeviceType Type { get; set; }
        public string Token { get; set; }

        public Device() { }
        public Device(DeviceType type, string token)
        {
            Type = type;
            Token = token;
        }
    }

    public enum DeviceType
    {
        Android, iOS
    }
}
