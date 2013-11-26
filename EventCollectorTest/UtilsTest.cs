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
            Utils.GetJstTime(utcTime).ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 10:12:00");
        }

        [TestMethod]
        public void タイムゾーンを日本に変更する2()
        {
            var utcTime = new DateTime(2013, 10, 7, 1, 12, 00, DateTimeKind.Utc);

            utcTime.ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 01:12:00");
            Utils.GetJstTime(utcTime).ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 10:12:00");
        }

        [TestMethod]
        public void タイムゾーンをUTCに変更する()
        {
            var jstTime = new DateTime(2013, 10, 7, 10, 12, 00);

            jstTime.ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 10:12:00");
            Utils.GetUtcTime(jstTime).ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 01:12:00");
        }


        [TestClass]
        public class Nullableな場合
        {
            [TestMethod]
            public void 入力utcがnullだった場合はnullを返す()
            {
                Utils.GetJstTime(null).IsNull();
            }

            [TestMethod]
            public void 入力jstがnullだった場合はnullを返す()
            {
                Utils.GetUtcTime(null).IsNull();
            }

            [TestMethod]
            public void 入力がnullでない場合はタイムゾーンを日本に変更した日付を返す()
            {
                DateTime? utcTime = new DateTime(2013, 10, 7, 1, 12, 00, DateTimeKind.Utc);

                utcTime.Value.ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 01:12:00");
                Utils.GetJstTime(utcTime).Value.ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 10:12:00");
            }

            [TestMethod]
            public void 入力がnullでない場合はタイムゾーンをUTCに変更した日付を返す()
            {
                DateTime? jstTime = new DateTime(2013, 10, 7, 10, 12, 00);

                jstTime.Value.ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 10:12:00");
                Utils.GetUtcTime(jstTime).Value.ToString("yyyy-MM-dd hh:mm:ss").Is("2013-10-07 01:12:00");
            }
        }
    }
}
