using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EventData
{
    public class CommonEvent : IComparable
    {
        public enum WebSvcType
        {
            Atnd,
            EventAtnd,
            DoorKeeper
        }
        public WebSvcType WebSvc { get; set; }
        public string Id { get; set; }
        [DisplayName("タイトル")]
        public string Title { get; set; }
        [DisplayName("開始")]
        public DateTime? StartedAt { get; set; }
        [DisplayName("終了")]
        public DateTime? EndedAt { get; set; }
        [DisplayName("住所")]
        public string Address { get; set; }
        [DisplayName("場所")]
        public string Place { get; set; }
        [DisplayName("説明")]
        public string Description { get; set; }
        public string OwnerNickname { get; set; }
        public string Url { get; set; }
        public string EventUrl { get; set; }

        public CommonEvent()
        {
        }

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
