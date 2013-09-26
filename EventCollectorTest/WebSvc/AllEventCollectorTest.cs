using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCollector.WebSvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventCollectorTest.WebSvc
{
    [TestClass]
    public class AllEventCollectorTest
    {
        [TestMethod]
        public void 全てのサービスからイベント取得()
        {
            var sut = new AllEventCollector();
            var result = sut.GetEvents(201307, "松山");

            result.Count().Is(4);
        }

    }
}
