using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventData;

namespace EventCollector.WebSvc
{
    public class AtndEventCollector
    {
        private const string BaseUrl = "http://api.atnd.org/events/?format=json";

        public int ReadCount { get; private set; }

        public AtndEventCollector()
        {
            ReadCount = 25;
        }

        public AtndEventCollector(int readCount)
        {
            ReadCount = readCount;
        }

        public IList<CommonEvent> GetEvents(int ym, string keyword)
        {
            var apiUrl = string.Format(BaseUrl + "&count={0}&keyword={1}&ym={2}", ReadCount, keyword, ym);

            var downloader = new WebDownloader();
            var str = downloader.DownloadString(apiUrl);

            return AtndJsonParser.Parse(str);
        }
    }
}
