using System;
using HelloService.Entities.Model;
using Microsoft.AspNetCore.Http;

namespace HelloService.Helper
{
    public static class UserHelper
    {
        public static Device GetDeviceFromHeader(IHeaderDictionary headers)
        {
            if (!(headers.ContainsKey("device_type") && headers.ContainsKey("device_id"))) return null;
            var deviceType = headers["device_type"].ToString();
            var device = new Device()
            {
                Token = headers["device_id"]
            };
            if ("android".Equals(deviceType.ToLower()))
                device.Type = DeviceType.Android;
            else
                device.Type = DeviceType.iOS;
            return device;
        }
    }
}
