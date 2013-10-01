using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using EventSearch.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventSearch;
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
            public void 存在しない年が指定された場合は今日の年を使用する()
            {
                // Act
                var result = _sut.Index(new SearchModels() { Year = 2021 }) as ViewResult;

                // Assert
                result.IsNotNull();
                var model = result.Model as SearchModels;
                model.IsNotNull();
                model.Year.Is(DateTime.Now.Year);
            }

            [TestMethod]
            public void 存在しない月が指定された場合は今日の月を使用する()
            {
                // Act
                var result = _sut.Index(new SearchModels() { Month = 13 }) as ViewResult;

                // Assert
                result.IsNotNull();
                var model = result.Model as SearchModels;
                model.IsNotNull();
                model.Month.Is(DateTime.Now.Month);
            }
        }

    }
}
