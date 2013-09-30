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
            var factory = new MockFactory();

            List<CommonEvent> mockResult;

            var mock1 = factory.CreateMock<BaseEventCollector>();
            mockResult = new List<CommonEvent>();
            mockResult.AddRange(new[]
                      {
                          new CommonEvent("Mock1Event1", null, null, "", "", "", "", "", ""),
                          new CommonEvent("Mock1Event2", null, null, "", "", "", "", "", ""),
                      });
            mock1.Expects.One.MethodWith(x => x.GetEvents(201307, "松山")).WillReturn(mockResult);

            var mock2 = factory.CreateMock<BaseEventCollector>();
            mockResult = new List<CommonEvent>();
            mockResult.AddRange(new[]
                      {
                          new CommonEvent("Mock2Event1", null, null, "", "", "", "", "", ""),
                          new CommonEvent("Mock2Event2", null, null, "", "", "", "", "", ""),
                          new CommonEvent("Mock2Event3", null, null, "", "", "", "", "", ""),
                      });
            mock2.Expects.One.MethodWith(x => x.GetEvents(201307, "松山")).WillReturn(mockResult);

            var sut = new AllEventCollector(new[] { mock1.MockObject, mock2.MockObject });
            var result = sut.GetEvents(201307, "松山");

            result.Count().Is(5);
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
