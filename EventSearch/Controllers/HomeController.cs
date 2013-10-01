using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCollector.WebSvc;
using EventSearch.Models;

namespace EventSearch.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(SearchModels model)
        {
            if (model.Year == 0 && model.Month == 0)
            {
                model.Year = DateTime.Now.Year;
                model.Month = DateTime.Now.Month;
                return View(model);
            }

            var eventCollector = new AllEventCollector();
            if (model.Year < 2010 || model.Year > 2020) model.Year = DateTime.Now.Year;
            if (model.Month < 1 || model.Month > 12) model.Month = DateTime.Now.Month;

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
