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
        private MockFactory _factory;
        private AllEventCollector sut;

        [TestInitialize]
        public void SetUp()
        {
            _factory = new MockFactory();

            List<CommonEvent> mockResult;

            var mock1 = _factory.CreateMock<BaseEventCollector>();
            mockResult = new List<CommonEvent>();
            mockResult.AddRange(new[]
                      {
                          new CommonEvent(0, "100", "Mock1Event1", new DateTime(2013,10,15), null, "", "", "", "", "", ""),
                          new CommonEvent(0, "100", "Mock1Event2", new DateTime(2013,10,1), null, "", "", "", "", "", ""),
                      });
            mock1.Expects.One.MethodWith(x => x.GetEvents(201307, "松山")).WillReturn(mockResult);

            var mock2 = _factory.CreateMock<BaseEventCollector>();
            mockResult = new List<CommonEvent>();
            mockResult.AddRange(new[]
                      {
                          new CommonEvent(0, "100", "Mock2Event1", new DateTime(2013,10,8), null, "", "", "", "", "", ""),
                          new CommonEvent(0, "100", "Mock2Event2", new DateTime(2013,10,30), null, "", "", "", "", "", ""),
                          new CommonEvent(0, "100", "Mock2Event3", new DateTime(2013,10,25), null, "", "", "", "", "", ""),
                      });
            mock2.Expects.One.MethodWith(x => x.GetEvents(201307, "松山")).WillReturn(mockResult);

            sut = new AllEventCollector(new[] { mock1.MockObject, mock2.MockObject });
        }

        [TestMethod]
        public void 全てのサービスからイベント取得()
        {
            var result = sut.GetEvents(201307, "松山");

            result.Count().Is(5);
        }

        [TestMethod]
        public void 取得したイベントは昇順にソートされる()
        {
            var result = sut.GetEvents(201307, "松山");

            (result[0].StartedAt.Value.Ticks <= result[1].StartedAt.Value.Ticks).IsTrue();
            (result[1].StartedAt.Value.Ticks <= result[2].StartedAt.Value.Ticks).IsTrue();
            (result[2].StartedAt.Value.Ticks <= result[3].StartedAt.Value.Ticks).IsTrue();
            (result[3].StartedAt.Value.Ticks <= result[4].StartedAt.Value.Ticks).IsTrue();
        }

        [TestMethod]
        public void NMockのサンプル()
        {
            var mock = _factory.CreateMock<BaseEventCollector>();

            var result = new List<CommonEvent>();
            result.AddRange(new[]
                      {
                          new CommonEvent(0, "100", "A", null, null, "", "", "", "", "", ""),
                          new CommonEvent(0, "100", "A", null, null, "", "", "", "", "", ""),
                      });


            mock.Expects.One.MethodWith(x => x.GetEvents(201307, "松山")).WillReturn(result);

            var sut = mock.MockObject;

            sut.GetEvents(201307, "松山").Count().Is(2);
        }

    }
}
