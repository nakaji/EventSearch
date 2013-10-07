using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventCollector.WebSvc;

namespace EventCollectorTest.WebSvc
{
    [TestClass]
    public class EventAtndJsonParserTest
    {
        private EventAtndJsonParser sut;

        [TestMethod]
        public void Jsonから共通イベントオブジェクトを作成する()
        {
            var str = File.ReadAllText(@"TestData\EventATND-松山-201307.json");

            var events = EventAtndJsonParser.Parse(str);

            events.Count().Is(9);
            events.ToArray()[0].Title.Is("第１回スマホサークル会合");
            events.ToArray()[0].StartedAt.Value.ToString("dd日 HH:mm").Is("26日 21:00");
            events.ToArray()[0].EndedAt.Value.ToString("dd日 HH:mm").Is("26日 22:00");
            ;
        }

        [TestMethod]
        public void 検索結果が0件の場合からのリストを返す()
        {
            var str = File.ReadAllText(@"TestData\EventATND-NO_DATA.json");

            var events = EventAtndJsonParser.Parse(str);

            events.Count().Is(0);
        }
    }
}
