﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using EventData;

namespace EventCollector.WebSvc
{
    public class EventAtndEventCollector : BaseEventCollector
    {
        private const string BaseUrl = "http://api.atnd.org/eventatnd/event/?format=json";

        public override IList<CommonEvent> GetEvents(int ym, string keyword)
        {
            var apiUrl = string.Format(BaseUrl + "&count={0}&keyword={1}&ym={2}", ReadCount, keyword, ym);

            var downloader = new WebDownloader { Encoding = Encoding.UTF8 };
            try
            {
                var str = downloader.DownloadString(apiUrl);
                return EventAtndJsonParser.Parse(str);
            }
            catch (WebException e)
            {
                return new List<CommonEvent>();
            }
        }
    }
}
