using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCollector
{
    public static class Utils
    {

        public static DateTime GetJapaneseTime(DateTime utcTime)
        {
            var tokyoStandardTime = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            var japaneseTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime.ToUniversalTime(), tokyoStandardTime);
            return japaneseTime;
        }

        public static DateTime? GetJapaneseTime(DateTime? utcTime)
        {
            if (utcTime == null)return null;

            return GetJapaneseTime(utcTime.Value);
        }
    }
}
