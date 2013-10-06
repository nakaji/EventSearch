using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCollector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventCollectorTest
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void タイムゾーンを日本に変更する()
        {
            var utcTime = new DateTime(2013, 10, 7, 1, 12, 00, DateTimeKind.Utc);

            utcTime.ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 01:12:00");
            Utils.GetJapaneseTime(utcTime).ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 10:12:00");
        }

        [TestClass]
        public class Nullableな場合
        {
            [TestMethod]
            public void 入力がnullだった場合はnullを返す()
            {
                Utils.GetJapaneseTime(null).IsNull();
            }

            [TestMethod]
            public void 入力がnullでない場合はタイムゾーンを日本に変更した日付を返す()
            {
                DateTime? utcTime = new DateTime(2013, 10, 7, 1, 12, 00, DateTimeKind.Utc);

                utcTime.Value.ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 01:12:00");
                Utils.GetJapaneseTime(utcTime).Value.ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 10:12:00");
            }
        }
    }
}
