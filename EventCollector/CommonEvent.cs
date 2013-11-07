using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventData
{
    public class CommonEvent:IComparable
    {
        public enum WebSvcType
        {
            Atnd,
            EventAtnd,
            DoorKeeper
        }
        public WebSvcType WebSvc { get; private set; }
        public string Id { get; private set; }
        public string Title { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public string Address { get; private set; }
        public string Place { get; private set; }
        public string Description { get; private set; }
        public string OwnerNickname { get; private set; }
        public string Url { get; private set; }
        public string EventUrl { get; private set; }

        public CommonEvent(
            WebSvcType webSvc,
            string id,
            string title,
            DateTime? startedAt,
            DateTime? endedAt,
            string address,
            string place,
            string description,
            string ownerNickname,
            string url,
            string eventUrl
            )
        {
            WebSvc = webSvc;
            Id = id;
            Title = title;
            StartedAt = startedAt;
            EndedAt = endedAt;
            Address = address;
            Place = place;
            Description = description;
            OwnerNickname = ownerNickname;
            Url = url;
            EventUrl = eventUrl;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is CommonEvent)) throw new ArgumentException();

            var e = obj as CommonEvent;

            if (e.StartedAt == null) return -1;
            if (StartedAt == null) return 1;

            if (StartedAt.Value.Ticks < e.StartedAt.Value.Ticks) return -1;
            if (StartedAt.Value.Ticks > e.StartedAt.Value.Ticks) return 1;

            return 0;
        }
    }
}
