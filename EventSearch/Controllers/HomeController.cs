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

        public ActionResult Login()
        {
            var clientId = ConfigurationManager.AppSettings.Get("client_id");
            var redirectUri = ConfigurationManager.AppSettings.Get("redirect_uri");
            var scope = HttpUtility.UrlEncode("https://www.googleapis.com/auth/calendar");

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
            var clientId = ConfigurationManager.AppSettings.Get("client_id");
            var clientSecret = ConfigurationManager.AppSettings.Get("client_secret");
            var redirectUri = ConfigurationManager.AppSettings.Get("redirect_uri");

            
            var cl = new WebClient();
            cl.Encoding = Encoding.UTF8;
            cl.Headers["content-type"] = "application/x-www-form-urlencoded";
            var query = String.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type={4}",
                code,
                clientId,
                "sHeOw4iYzvPaQbU8qSkotY0y",
                    "http://localhost:51661/Home/Auth",
                "authorization_code"
                );
            var result = cl.UploadString("https://accounts.google.com/o/oauth2/token", "POST", query);

            var account = DynamicJson.Parse(result);

            Session.Add("access_token", account.access_token);

            Response.Redirect("~/");
            return null;
        }
    }
}
