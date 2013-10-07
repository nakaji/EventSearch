using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventCollector.WebSvc;

namespace EventCollectorTest.WebSvc
{
    [TestClass]
    public class EventAtndEventCollectorTest
    {
        [TestClass]
        public class コンストラクタ
        {
            [TestMethod]
            public void デフォルトでは読み込み件数は25件()
            {
                var sut = new EventAtndEventCollector();

                sut.ReadCount.Is(25);
            }
        }

        [TestMethod]
        public void イベント取得()
        {
            var sut = new EventAtndEventCollector();
            var result = sut.GetEvents(201307, "松山");

            result.Count().Is(9);
            // 2013年7月にかからないイベントは存在しない
            result.Any(x => x.EndedAt < new DateTime(2013, 07, 01) && x.StartedAt >= new DateTime(2013, 08, 01)).IsFalse();
        }
    }
}
