﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Codeplex.Data;
using EventCollector.WebSvc;
using EventData;
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
                ModelState.Clear();
                return View(model);
            }

            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                var collector = new AllEventCollector();
                var evemts = collector.GetEvents(model.Year * 100 + model.Month, model.Keyword);
                model.Events.AddRange(evemts);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AddEvent(CommonEvent.WebSvcType webSvc, string id, string title, DateTime startedAtUtcTime,
            DateTime endedAtUtcTime, string address, string place, string ownerNickname, string url, string eventUrl)
        {
            DateTime startedAt = EventCollector.Utils.GetJstTime(startedAtUtcTime);
            DateTime endedAt = EventCollector.Utils.GetJstTime(endedAtUtcTime);
            var description = string.Format("{0}\n" + "日時：{1}\n" + "住所：{2} {3}", eventUrl, startedAt.ToString("yyyy/MM/dd hh:mm"), address, place);

            var viewModel = new AddCalendarViewModel();
            viewModel.Event = new CommonEvent(webSvc, id, title, startedAt, endedAt, address, place, description, ownerNickname, url, eventUrl);
            var api = new GoogleApis(Session["access_token"].ToString());
            viewModel.CalendarList = api.GetCalendarList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEvent(AddCalendarViewModel model)
        {
            var api = new GoogleApis(Session["access_token"].ToString());
            api.AddEvent(model.CalendarId, model.Event, model.TimeZoneOffset);

            return Redirect("~/");
        }
    }
}
