using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace EventSearch.Models
{
    public class GoogleApis
    {
        public string AccessToken { get; private set; }

        public GoogleApis(string accessToken)
        {
            AccessToken = accessToken;
        }

        public GoogleApis()
        {
        }

        public void Auth(string code)
        {
            var clientId = ConfigurationManager.AppSettings.Get("client_id");
            var clientSecret = ConfigurationManager.AppSettings.Get("client_secret");
            var redirectUri = ConfigurationManager.AppSettings.Get("redirect_uri");

            using (var cl = new WebClient())
            {
                cl.Encoding = Encoding.UTF8;
                cl.Headers["content-type"] = "application/x-www-form-urlencoded";
                var query = String.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type={4}",
                    code,
                    clientId,
                    clientSecret,
                    redirectUri,
                    "authorization_code"
                    );
                var result = cl.UploadString("https://accounts.google.com/o/oauth2/token", "POST", query);
                var account = DynamicJson.Parse(result);

                AccessToken = account.access_token;
            }
        }

        public UserInfo GetUserInfo()
        {
            using (var cl = new WebClient { Encoding = Encoding.UTF8 })
            {
                var url = String.Format("https://www.googleapis.com/oauth2/v1/userinfo?access_token={0}", AccessToken);
                var result = cl.DownloadString(url);
                var userInfo = DynamicJson.Parse(result);

                return new UserInfo(userInfo.id, userInfo.name, userInfo.given_name, userInfo.family_name, userInfo.picture);
            }
        }
    }
}