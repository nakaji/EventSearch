using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Codeplex.Data;
using EventData;

namespace EventCollector.WebSvc
{
    public class DoorkeeperJsonParser
    {
        public static IList<CommonEvent> Parse(string str)
        {
            var events = GetEventList(str);
            return events;
        }

        /// <summary>
        /// Jsonからイベントリストを作成
        /// </summary>
        /// <param name="str">Json形式の文字列</param>
        /// <returns>イベントリスト</returns>
        private static IList<CommonEvent> GetEventList(string str)
        {
            var root = DynamicJson.Parse(str);

            var tst = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            var events = new List<CommonEvent>();
            DateTime japaneseTime;
            foreach (var item in root)
            {
                DateTime? startedAt = null;
                var dateTime = ParseDateTime(item.@event.starts_at);
                if (dateTime != null)
                {
                    japaneseTime = TimeZoneInfo.ConvertTimeFromUtc(ParseDateTime(item.@event.starts_at).ToUniversalTime(), tst);
                    startedAt = japaneseTime;
                }
                DateTime? endsAt = null;
                dateTime = ParseDateTime(item.@event.starts_at);
                if (dateTime != null)
                {
                    japaneseTime = TimeZoneInfo.ConvertTimeFromUtc(ParseDateTime(item.@event.ends_at).ToUniversalTime(), tst);
                    endsAt = japaneseTime;
                }
                events.Add(new CommonEvent(
                    item.@event.title,
                    startedAt,
                    endsAt,
                    item.@event.address,
                    item.@event.venue_name,
                    item.@event.description,
                    item.@event.group.name,
                    item.@event.group.public_url,
                    item.@event.public_url
                    ));
            }
            return events;
        }

        public static DateTime? ParseDateTime(string str)
        {
            DateTime wk;
            if (DateTime.TryParse(str, out wk))
            {
                return wk;
            }
            else
            {
                return null;
            }
        }


    }
}
