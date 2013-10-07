using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using EventSearch.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventCollector;
using EventSearch.Controllers;

namespace EventSearch.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestClass]
        public class Index
        {
            private HomeController _sut;

            [TestInitialize]
            public void SetUp()
            {
                _sut = new HomeController();
            }

            [TestMethod]
            public void 初期状態では今日の年月をセットする()
            {
                // Act
                var result = _sut.Index(new SearchModels()) as ViewResult;

                // Assert
                result.IsNotNull();
                var model = result.Model as SearchModels;
                model.IsNotNull();
                model.Year.Is(DateTime.Now.Year);
                model.Month.Is(DateTime.Now.Month);
            }

            [TestMethod]
            public void 存在しない年が指定された場合はエラー()
            {
                _sut.ControllerContext = new ControllerContext();

                // Act
                var result = _sut.Index(new SearchModels() { Year = 2021, Month = 10, Keyword = "hoge" }) as ViewResult;
                
                // Assert
                result.IsNotNull();
                var model = result.Model as SearchModels;

                model.IsNotNull();
                _sut.ModelState.IsValidField("Year").IsFalse();
                _sut.ModelState.IsValidField("Month").IsTrue();
                _sut.ModelState.IsValidField("Keyword").IsTrue();
            }

            [TestMethod]
            public void 存在しない月が指定された場合はエラー()
            {
                _sut.ControllerContext = new ControllerContext();

                // Act
                var result = _sut.Index(new SearchModels() { Year = 2013, Month = 13, Keyword = "hoge" }) as ViewResult;

                // Assert
                result.IsNotNull();
                var model = result.Model as SearchModels;
                model.IsNotNull();
                _sut.ModelState.IsValidField("Year").IsTrue();
                _sut.ModelState.IsValidField("Month").IsFalse();
                _sut.ModelState.IsValidField("Keyword").IsTrue();
            }

            [TestMethod]
            public void イベントの検索結果をモデルに含めて返却する()
            {
                _sut.ControllerContext = new ControllerContext();
                
                // Act
                var result = _sut.Index(new SearchModels() { Year = 2013, Month = 7, Keyword = "松山" }) as ViewResult;

                // Assert
                result.IsNotNull();
                var model = result.Model as SearchModels;
                Assert.IsTrue(model.Events.Any());
            }
        }

    }
}
