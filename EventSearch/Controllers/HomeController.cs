using System;
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult AddCalendar(CommonEvent.WebSvcType webSvc, string id, string title, DateTime startedAt,
            DateTime endedAt, string address, string place, string ownerNickname, string url, string eventUrl)
        {
            var description = string.Format("{0}\n" + "日時：{1}\n" + "住所：{2} {3}", eventUrl, startedAt.ToString("yyyy/MM/dd hh:mm"), address, place);
            var e = new CommonEvent(webSvc, id, title, startedAt, endedAt, address, place, description, ownerNickname,
                url, eventUrl);
            return View(e);
        }

        [HttpPost]
        public ActionResult AddCalendar(CommonEvent e)
        {
            var api = new GoogleApis(Session["access_token"].ToString());
            api.AddCalendar(e);

            Response.Redirect("~/");
            return null;
        }

        public ActionResult Login()
        {
            var clientId = ConfigurationManager.AppSettings.Get("client_id");
            var redirectUri = ConfigurationManager.AppSettings.Get("redirect_uri");
            var scope = HttpUtility.UrlEncode("https://www.googleapis.com/auth/calendar") + "+" +
                        HttpUtility.UrlEncode("https://www.googleapis.com/auth/userinfo.profile");

            var addr = string.Format("https://accounts.google.com/o/oauth2/auth?client_id={0}&response_type=code&redirect_uri={1}&scope={2}",
                                    clientId,
                                    redirectUri,
                                    scope
                                );

            Response.Redirect(addr);
            return null;
        }

        public ActionResult Logout()
        {
            Session["access_token"] = null;

            Response.Redirect("~/");
            return null;
        }

        public ActionResult Auth(string code)
        {
            var apis = new GoogleApis();

            apis.Auth(code);
            Session.Add("access_token", apis.AccessToken);

            var userInfo = apis.GetUserInfo();
            Session.Add("user_info", userInfo);

            Response.Redirect("~/");
            return null;
        }
    }
}
