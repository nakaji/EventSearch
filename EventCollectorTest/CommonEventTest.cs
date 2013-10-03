using System;
using System.Text;
using System.Collections.Generic;
using EventData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventCollectorTest
{
    /// <summary>
    /// CommonEventTest の概要の説明
    /// </summary>
    [TestClass]
    public class CommonEventTest
    {
        [TestClass]
        public class CompareToのテスト
        {
            private CommonEvent sut;

            [TestInitialize()]
            public void MyTestInitialize()
            {
                sut = new CommonEvent("TEST", new DateTime(2013, 10, 1, 13, 0, 0), null, "", "", "", "", "", "");
            }

            [TestMethod]
            public void テスト対象の方が早く開催される場合()
            {
                var e = new CommonEvent("TEST", new DateTime(2013, 10, 2), null, "", "", "", "", "", "");

                sut.CompareTo(e).Is(-1);
            }

            [TestMethod]
            public void テスト対象の方が遅く開催される場合()
            {
                var e = new CommonEvent("TEST", new DateTime(2013, 9, 30), null, "", "", "", "", "", "");

                sut.CompareTo(e).Is(1);
            }

            [TestMethod]
            public void テスト対象と同じ日時に開催される場合()
            {
                var e = new CommonEvent("TEST", new DateTime(2013, 10, 1, 13, 0, 0), null, "", "", "", "", "", "");

                sut.CompareTo(e).Is(0);
            }

            [TestMethod]
            public void 比較対象の開催日時がnullの場合()
            {
                var e = new CommonEvent("TEST", null, null, "", "", "", "", "", ""); ;
                sut.CompareTo(e).Is(-1);
            }

            [TestMethod]
            public void テスト対象の開催日時がnullの場合()
            {
                var e = new CommonEvent("TEST", new DateTime(2013, 10, 1, 13, 0, 0), null, "", "", "", "", "", "");
                sut = new CommonEvent("TEST", null, null, "", "", "", "", "", ""); ;

                sut.CompareTo(e).Is(1);
            }

            [TestMethod]
            [ExpectedException(typeof(System.ArgumentException))]
            public void 比較対象がCommonEvent型じゃなかった場合()
            {
                sut.CompareTo(new object());
            }
        }
    }
}
