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
            var events = new List<CommonEvent>();
            if (str.Equals("[]")) return events;
            
            var root = DynamicJson.Parse(str);

            foreach (var item in root)
            {
                events.Add(new CommonEvent(
                    item.@event.title,
                    Utils.GetJapaneseTime(ParseDateTime(item.@event.starts_at)),
                    Utils.GetJapaneseTime(ParseDateTime(item.@event.ends_at)),
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
