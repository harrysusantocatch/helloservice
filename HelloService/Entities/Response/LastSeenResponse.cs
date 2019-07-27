using HelloService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloService.Entities.Response
{
    public class LastSeenResponse
    {
        public string StrDate { get; set; }

        public LastSeenResponse(string strDate, string gmt)
        {
            if(strDate != null)
            {
                if ("Online".Equals(strDate)) StrDate = strDate;
                else
                {
                    long longDate = long.Parse(strDate);
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    DateTime date = start.AddMilliseconds(longDate).ToLocalTime();
                    StrDate = TimeConverter.GetDisplayDate(date, gmt);
                }
            }
        }
    }
}
