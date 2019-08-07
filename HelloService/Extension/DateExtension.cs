using System;
using HelloService.Helper;

namespace HelloService.Extension
{
    public static class DateExtension
    {
        /// <summary>
        /// https://stackoverflow.com/questions/3354893/how-can-i-convert-a-datetime-to-the-number-of-seconds-since-1970
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToUnixLong(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return (long)Math.Floor(diff.TotalMilliseconds);
        }

        public static DateTime ConvertToServerTime(this DateTime self, string fromGMT)
        {
            return TimeConverter.ConvertToServerTime(self, fromGMT);
        }

        public static DateTime ToActualValue(this DateTime? date)
        {
            if (date == null) return new DateTime(0L);
            else return date.Value;
        }

        public static bool IsNull(this DateTime? date)
        {
            if (date == null) return false;
            else return date.Value.Ticks == 0L;
        }

        public static bool IsNull(this DateTime date)
        {
            return date.Ticks == 0L;
        }

    }
}