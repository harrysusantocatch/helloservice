using System;
using System.Security.Claims;
using HelloService.DataAccess.Implement;
using HelloService.Entities.DB;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerExtension
    {
        public static User GetUserAuthorize<T>(this T self) where T : Controller
        {
            if (!self.HttpContext.User.Identity.IsAuthenticated) return null;
            var userId = self.HttpContext.User.GetObjectId();
            var user = new UserDao().FindByID(userId);
            if (user == null || user.Device == null || (user.Device.Token != self.HttpContext.User.GetSerialNumber()) || !user.Active)
                return null;
            return user;
        }
    }


    public static class IdentityExtension
    {

        public static string GetName(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, "name");
        }

        public static string GetPhoneNumber(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, "phone_number");
        }

        public static string GetUid(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, "user_id");
        }

        public static string GetProfilePicture(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, "picture");
        }

        public static DateTime GetExpirationDateTime(this ClaimsPrincipal current)
        {
            var timestamp = long.Parse(GetClaimValue(current, "exp"));
            var datetime = DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
            return datetime;
        }

        public static string GetEmail(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, ClaimTypes.Email);
        }

        public static string GetUserName(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, ClaimTypes.Name);
        }

        public static string GetObjectId(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, ClaimTypes.Sid);
        }

        public static string GetMobilePhone(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, ClaimTypes.MobilePhone);
        }

        public static string GetSerialNumber(this ClaimsPrincipal current)
        {
            return GetClaimValue(current, ClaimTypes.SerialNumber);
        }

        private static string GetClaimValue(ClaimsPrincipal principal, string type)
        {
            var enumerator = principal.Claims.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current != null)
                {
                    if (current.Type.Equals(type))
                    {
                        return current.Value;
                    }
                }
            }
            return null;
        }
    }
}
