using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCollector.WebSvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventCollector;

namespace EventCollectorTest.WebSvc
{
    [TestClass]
    public class AtndEventCollectorTest
    {
        [TestClass]
        public class コンストラクタ
        {
            [TestMethod]
            public void デフォルトでは読み込み件数は25件()
            {
                var sut = new AtndEventCollector();

                sut.ReadCount.Is(25);
            }

            [TestMethod]
            public void 読み込み件数をコンストラクタで指定できる()
            {
                var sut = new AtndEventCollector(50);

                sut.ReadCount.Is(50);
            }
        }

        [TestMethod]
        public void イベント取得()
        {
            var sut = new AtndEventCollector();
            var result = sut.GetEvents(201307, "松山");

            result.Count().IsNot(0);
            // 2013年7月にかからないイベントは存在しない
            result.Any(x => x.EndedAt < new DateTime(2013, 07, 01) && x.StartedAt >= new DateTime(2013, 08, 01)).IsFalse();
        }
    }
}
