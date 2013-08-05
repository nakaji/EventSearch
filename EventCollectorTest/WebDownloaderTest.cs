using EventCollector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventCollectorTest
{
    [TestClass]
    public class WebDownloaderTest
    {
        [TestMethod]
        public void 指定されたurlから文字列を取得する()
        {
            var sut = new WebDownloader();

            var str = sut.DownloadString("http://www.bing.com/");

            str.Contains("<html").IsTrue();
        }
    }
}
