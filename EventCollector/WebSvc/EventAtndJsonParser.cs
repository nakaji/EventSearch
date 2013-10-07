using System;
using System.Collections.Generic;
using Codeplex.Data;
using EventData;

namespace EventCollector.WebSvc
{
    public class EventAtndJsonParser
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
            var events = new List<CommonEvent>();

            if (root.results_returned == 0) return events;

            foreach (var item in root.events[0].@event)
            {
                events.Add(new CommonEvent(
                    item.title,
                    Utils.GetJapaneseTime(ParseDateTime(item.started_at)),
                    Utils.GetJapaneseTime(ParseDateTime(item.ended_at)),
                    item.address,
                    item.place,
                    item.description,
                    item.owner_nickname,
                    item.url,
                    item.event_url
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
