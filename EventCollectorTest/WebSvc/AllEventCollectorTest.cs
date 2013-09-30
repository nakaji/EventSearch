using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCollector.WebSvc;
using EventData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;

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
        
        [TestMethod]
        public void NMockのサンプル()
        {
            var factory = new MockFactory();
            var mock = factory.CreateMock<BaseEventCollector>();

            var result = new List<CommonEvent>();
            result.AddRange(new CommonEvent[]
                      {
                          new CommonEvent("A", null, null, "", "", "", "", "", ""),
                          new CommonEvent("A", null, null, "", "", "", "", "", ""),
                      });


            mock.Expects.One.MethodWith(x => x.GetEvents(201307, "松山")).WillReturn(result);

            var sut = mock.MockObject;

            sut.GetEvents(201307, "松山").Count();
        }

    }
}
