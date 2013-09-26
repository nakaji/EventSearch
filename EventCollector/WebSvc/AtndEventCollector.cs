using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventData;

namespace EventCollector.WebSvc
{
    public class AtndEventCollector : EventCollector
    {
        private const string BaseUrl = "http://api.atnd.org/events/?format=json";

        public AtndEventCollector()
        {
        }

        public AtndEventCollector(int readCount)
        {
            ReadCount = readCount;
        }

        public override IList<CommonEvent> GetEvents(int ym, string keyword)
        {
            var apiUrl = string.Format(BaseUrl + "&count={0}&keyword={1}&ym={2}", ReadCount, keyword, ym);

            var downloader = new WebDownloader();
            var str = downloader.DownloadString(apiUrl);

            return AtndJsonParser.Parse(str);
        }
    }
}
