using System.Net;
using System.Text;

namespace EventCollector
{
    public class WebDownloader
    {
        private Encoding _encoding = Encoding.Default;

        public Encoding Encoding
        {
            get { return _encoding; } 
            set { _encoding = value; }
        }

        public virtual string DownloadString(string url)
        {
            using (var cl = new WebClient())
            {
                cl.Encoding = Encoding;
                var str = cl.DownloadString(url);
                return str;
            }
        }
    }
}
