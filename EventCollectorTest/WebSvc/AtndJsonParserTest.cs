using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCollector;
using EventCollector.WebSvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventCollectorTest.WebSvc
{
    [TestClass]
    public class AtndJsonParserTest
    {
        private AtndJsonParser sut;

        [TestMethod]
        public void Jsonから共通イベントオブジェクトを作成する()
        {
            var str = File.ReadAllText(@"TestData\ATND-松山-201307.json");

            var events = AtndJsonParser.Parse(str);

            events.Count().Is(3);
            events.ToArray()[0].Title.Is("情報交流会");
            events.ToArray()[0].StartedAt.Value.ToString("dd日 HH:mm").Is("21日 17:30");
            events.ToArray()[0].EndedAt.Value.ToString("dd日 HH:mm").Is("21日 20:00");
            events.ToArray()[2].Title.Is("Webクリエイターに足りない、本当のSEOスキル in 松山")
            ;
        }

        [TestMethod]
        public void 検索結果が0件の場合からのリストを返す()
        {
            var str = File.ReadAllText(@"TestData\ATND-NO_DATA.json");

            var events = AtndJsonParser.Parse(str);

            events.Count().Is(0);
        }
    }
}
