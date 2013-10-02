using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
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

            var collector = new AllEventCollector();
            var evemts = collector.GetEvents(model.Year * 100 + model.Month, model.Keyword);
            model.Events.AddRange(evemts);

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
