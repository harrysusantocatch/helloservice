using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.Helper
{
    public static class Constant
    {
        public const string DATABASE_NAME = "HELLO_DB";
        public const string KEY_ENCRYPT = "HELLO_DB";
        public static DateTime SERVER_TIME => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, SERVER_TIME_ZONE);
        public static TimeZoneInfo SERVER_TIME_ZONE => TimeConverter.TimeZoneInfoGetter(SERVER_GMT);
        public static string SERVER_GMT => "+07:00";

        public static long DEFAULT_LASTDATE = 0;
    }
}
