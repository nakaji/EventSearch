using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventData;
using Newtonsoft.Json;

namespace EventCollector.WebSvc
{
    public class AtndJsonParser
    {
        public static IList<CommonEvent> Parse(string str)
        {
            var root = JsonConvert.DeserializeObject<Rootobject>(str);

            var events = new List<CommonEvent>();
            foreach (var item in root.events)
            {
                DateTime? startedAt = item.started_at;
                DateTime? endedAt = item.ended_at;

                events.Add(new CommonEvent(item.title, startedAt, endedAt,
                                     item.address, item.place, item.description, item.owner_nickname, item.url,
                                     item.event_url));
            }
            return events;
        }


        public class Rootobject
        {
            public int results_start { get; set; }
            public Event[] events { get; set; }
            public int results_available { get; set; }
            public int results_returned { get; set; }
        }

        public class Event
        {
            public DateTime? started_at { get; set; }
            public float? lat { get; set; }
            public string event_url { get; set; }
            public float? lon { get; set; }
            public string description { get; set; }
            public int event_id { get; set; }
            public DateTime? updated_at { get; set; }
            public string title { get; set; }
            public string hash_tag { get; set; }
            public int owner_id { get; set; }
            public string owner_twitter_img { get; set; }
            public string url { get; set; }
            public string place { get; set; }
            public object deleted_at { get; set; }
            public bool confined_biziq { get; set; }
            public string _catch { get; set; }
            public string owner_twitter_id { get; set; }
            public string address { get; set; }
            public int waiting { get; set; }
            public int accepted { get; set; }
            public string recommended_twitter_account { get; set; }
            public int? limit { get; set; }
            public DateTime? ended_at { get; set; }
            public string owner_nickname { get; set; }
        }
    }
}
