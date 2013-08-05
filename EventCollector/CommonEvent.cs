using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventData
{
    public class CommonEvent
    {
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
    }
}
