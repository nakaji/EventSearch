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
    public class DoorkeeperJsonParserTest
    {
        private DoorkeeperJsonParser sut;

        [TestMethod]
        public void Jsonから共通イベントオブジェクトを作成する()
        {
            var str = File.ReadAllText(@"TestData\Doorkeeper-松山-201307.json");

            var events = DoorkeeperJsonParser.Parse(str);

            events.Count().Is(1);
            events.ToArray()[0].Title.Is("Agile459 アジャイルサムライ読書会松山道場 #15 『ラストサムライ』");
            ;
        }
    }
}
