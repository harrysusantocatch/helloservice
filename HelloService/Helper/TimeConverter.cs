using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TimeZoneConverter;

namespace HelloService.Helper
{
    public static class TimeConverter
    {
        private static readonly Dictionary<string, string> TimeZones = new Dictionary<string, string>
        {
            { "+00.00", "GMT Standard Time" },
            { "+01.00", "W. Europe Standard Time" },
            { "+02.00", "Jordan Standard Time" },
            { "+03.00", "Arabic Standard Time" },
            { "+03.30", "Iran Standard Time" },
            { "+04.00", "Arabian Standard Time" },
            { "+04.30", "Afghanistan Standard Time" },
            { "+05.00", "Ekaterinburg Standard Time" },
            { "+05.30", "India Standard Time" },
            { "+05.45", "Nepal Standard Time" },
            { "+06.00", "N. Central Asia Standard Time" },
            { "+06.30", "Myanmar Standard Time" },
            { "+07.00", "SE Asia Standard Time" },
            { "+08.00", "China Standard Time" },
            { "+09.00", "Tokyo Standard Time" },
            { "+09.30", "Cen. Australia Standard Time" },
            { "+10.00", "E. Australia Standard Time" },
            { "+11.00", "Central Pacific Standard Time" },
            { "+12.00", "New Zealand Standard Time" },
            { "+13.00", "Tonga Standard Time" },
            { "-01.00", "Azores Standard Time" },
            { "-02.00", "Mid-Atlantic Standard Time" },
            { "-03.00", "E. South America Standard Time" },
            { "-03.30", "Newfoundland Standard Time" },
            { "-04.00", "Atlantic Standard Time" },
            { "-04.30", "Venezuela Standard Time" },
            { "-05.00", "SA Pacific Standard Time" },
            { "-06.00", "Central America Standard Time" },
            { "-07.00", "US Mountain Standard Time" },
            { "-08.00", "Pacific Standard Time" },
            { "-09.00", "Alaskan Standard Time" },
            { "-10.00", "Hawaiian Standard Time" },
            { "-11.00", "Samoa Standard Time" },
            { "-12.00", "Dateline Standard Time" }
        };

        public static TimeZoneInfo TimeZoneInfoGetter(string fromGmt)
        {
            var gmt = ReformatGMT(fromGmt);
            var windowsTimeZoneId = TimeZones[gmt];
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId);
            }
            catch (Exception)
            {
                var linuxTimeZoneId = TZConvert.WindowsToIana(windowsTimeZoneId);
                return TimeZoneInfo.FindSystemTimeZoneById(linuxTimeZoneId);
            }
        }

        public static DateTime ConvertToServerTime(DateTime time, string fromGmt)
        {
            fromGmt = ReformatGMT(fromGmt);
            var timeZone = TimeZoneInfoGetter(fromGmt);
            return ConvertFromUtc(time, Constant.SERVER_TIME_ZONE, timeZone);
        }

        public static DateTime ConvertFromServerTime(DateTime time, string toGmt)
        {
            toGmt = ReformatGMT(toGmt);
            var timeZone = TimeZoneInfoGetter(toGmt);
            return ConvertFromUtc(time, timeZone, Constant.SERVER_TIME_ZONE);
        }

        public static DateTime ConvertFromUtc(DateTime time, TimeZoneInfo fromGMT, TimeZoneInfo toGMT)
        {
            var toSpan = fromGMT.BaseUtcOffset;
            var fromSpan = toGMT.BaseUtcOffset;

            var span = toSpan - fromSpan;
            return time.Add(span);
        }

        public static DateTime ConvertFromUtc(DateTime time, string fromGMT, string toGMT)
        {
            var toSpan = TimeZoneInfoGetter(toGMT).BaseUtcOffset;
            var fromSpan = TimeZoneInfoGetter(fromGMT).BaseUtcOffset;

            var span = toSpan - fromSpan;
            return time.Add(span);
        }

        private static string ReformatGMT(string gmt)
        {
            var result = gmt.Replace(':', '.');
            if (!result.Contains("+") && !result.Contains("-")) result = ("+" + result).Replace(" ", "");
            if (Regex.IsMatch(result, "^(\\+|-)\\d\\.\\d\\d$"))
            {
                result = Regex.Replace(result, "^\\+", "+0");
                result = Regex.Replace(result, "^-", "-0");
            }
            if (!TimeZones.ContainsKey(result)) throw new ArgumentException("Invalid GMT : " + gmt + "\nand after trying to reformat it : " + result);
            return result;
        }
    }
}
