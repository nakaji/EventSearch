﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using EventData;

namespace EventCollector.WebSvc
{
    public class DoorkeeperEventCollector : BaseEventCollector
    {
        private const string BaseUrl = "http://api.doorkeeper.jp/events?sort=starts_at&locale=ja";

        public override IList<CommonEvent> GetEvents(int ym, string keyword)
        {
            var since = new DateTime((int)Math.Floor(ym / (decimal)100), ym % 100, 1);
            var until = since.AddMonths(1).AddDays(-1);
            var apiUrl = string.Format(BaseUrl + "&since={0}&until={1}&q={2}", since.ToString("O"), until.ToString("O"), keyword);

            var downloader = new WebDownloader {Encoding = Encoding.UTF8};
            try
            {
                var str = downloader.DownloadString(apiUrl);
                return DoorkeeperJsonParser.Parse(str);
            }
            catch (WebException e)
            {
                return new List<CommonEvent>();
            }
        }
    }
}
