﻿using System;
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

        [TestInitialize]
        public void SetUp()
        {
            _factory = new MockFactory();
        }

        [TestMethod]
        public void 全てのサービスからイベント取得()
        {
            List<CommonEvent> mockResult;

            var mock1 = _factory.CreateMock<BaseEventCollector>();
            mockResult = new List<CommonEvent>();
            mockResult.AddRange(new[]
                      {
                          new CommonEvent("Mock1Event1", null, null, "", "", "", "", "", ""),
                          new CommonEvent("Mock1Event2", null, null, "", "", "", "", "", ""),
                      });
            mock1.Expects.One.MethodWith(x => x.GetEvents(201307, "松山")).WillReturn(mockResult);

            var mock2 = _factory.CreateMock<BaseEventCollector>();
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
            var mock = _factory.CreateMock<BaseEventCollector>();

            var result = new List<CommonEvent>();
            result.AddRange(new[]
                      {
                          new CommonEvent("A", null, null, "", "", "", "", "", ""),
                          new CommonEvent("A", null, null, "", "", "", "", "", ""),
                      });


            mock.Expects.One.MethodWith(x => x.GetEvents(201307, "松山")).WillReturn(result);

            var sut = mock.MockObject;

            sut.GetEvents(201307, "松山").Count().Is(2);
        }

    }
}
