using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace EventSearch.Models
{
    public class GoogleApis
    {
        private readonly string _accessToken ;

        public GoogleApis(string accessToken)
        {
            _accessToken = accessToken;
        }

        public UserInfo GetUserInfo()
        {
            var cl = new WebClient {Encoding = Encoding.UTF8};
            var url = String.Format("https://www.googleapis.com/oauth2/v1/userinfo?access_token={0}", _accessToken);
            var result = cl.DownloadString(url);
            var userInfo = DynamicJson.Parse(result);
            
            return new UserInfo(userInfo.id, userInfo.name, userInfo.given_name, userInfo.family_name, userInfo.picture);
        }
    }
}