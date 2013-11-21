using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventSearch.Models;

namespace EventSearch.Controllers
{
    public class AuthController : Controller
    {
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

            return Redirect(addr);
        }

        public ActionResult Logout()
        {
            Session["access_token"] = null;

            return Redirect("~/");
        }

        public ActionResult Google(string code)
        {
            var apis = new GoogleApis();

            apis.Auth(code);
            Session.Add("access_token", apis.AccessToken);

            var userInfo = apis.GetUserInfo();
            Session.Add("user_info", userInfo);

            return Redirect("~/");
        }
    }
}
