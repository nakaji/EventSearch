using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCollector
{
    public static class Utils
    {
        /// <summary>
        /// UTCの日付をJSTの日付に変換して返す
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        public static DateTime GetJstTime(DateTime utcTime)
        {
            var tokyoStandardTime = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            var japaneseTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tokyoStandardTime);
            return japaneseTime;
        }

        public static DateTime? GetJstTime(DateTime? utcTime)
        {
            if (utcTime == null) return null;

            return GetJstTime(utcTime.Value);
        }

        /// <summary>
        /// JSTの日付をUTCの日付に変換して返す
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        public static DateTime GetUtcTime(DateTime jstTime)
        {
            //var tokyoStandardTime = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            //var utcTime = TimeZoneInfo.ConvertTimeToUtc(jstTime, tokyoStandardTime);
            return jstTime.ToUniversalTime();
        }

        public static DateTime? GetUtcTime(DateTime? jstTime)
        {
            if (jstTime == null) return null;

            return GetUtcTime(jstTime.Value);
        }

    }
}
