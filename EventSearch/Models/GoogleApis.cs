using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using Codeplex.Data;
using EventData;

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

            var query = String.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type={4}",
                code,
                clientId,
                clientSecret,
                redirectUri,
                "authorization_code"
                );

            using (var cl = new WebClient { Encoding = Encoding.UTF8 })
            {
                cl.Headers["content-type"] = "application/x-www-form-urlencoded";
                var result = cl.UploadString("https://accounts.google.com/o/oauth2/token", "POST", query);
                var account = DynamicJson.Parse(result);

                AccessToken = account.access_token;
            }
        }

        public UserInfo GetUserInfo()
        {
            var url = String.Format("https://www.googleapis.com/oauth2/v1/userinfo?access_token={0}", AccessToken);

            using (var cl = new WebClient { Encoding = Encoding.UTF8 })
            {
                var result = cl.DownloadString(url);
                var userInfo = DynamicJson.Parse(result);

                return new UserInfo(userInfo.id, userInfo.name, userInfo.given_name, userInfo.family_name, userInfo.picture);
            }
        }

        public void AddEvent(string calendarId, CommonEvent e, int timeZoneOffset)
        {
            string start, end;

            start = XmlConvert.ToString(e.StartedAt.Value.AddMinutes(timeZoneOffset), XmlDateTimeSerializationMode.Utc);
            end = XmlConvert.ToString(e.EndedAt.Value.AddMinutes(timeZoneOffset), XmlDateTimeSerializationMode.Utc);
            var query = DynamicJson.Serialize(
                new
                   {
                       summary = e.Title,
                       description = e.Description,
                       location = e.Place,
                       start = new { dateTime = start },
                       end = new { dateTime = end },
                   });
            var url = string.Format("https://www.googleapis.com/calendar/v3/calendars/{0}", calendarId);

            using (var cl = new WebClient { Encoding = Encoding.UTF8 })
            {
                cl.Headers.Add("Authorization", "Bearer " + AccessToken);
                cl.Headers.Add("content-type", "application/json");

                cl.UploadString(url + "/events", "POST", query);
            }
        }

        public List<CalendarInfo> GetCalendarList()
        {
            const string url = "https://www.googleapis.com/calendar/v3/users/me/calendarList?minAccessRole=writer";

            using (var cl = new WebClient { Encoding = Encoding.UTF8 })
            {
                cl.Headers.Add("Authorization", "Bearer " + AccessToken);
                var result = DynamicJson.Parse(cl.DownloadString(url));

                var list = new List<CalendarInfo>();
                foreach (var cal in result.items)
                {
                    list.Add(new CalendarInfo() { Id = cal.id, Summary = cal.summary });
                }

                return list;
            }
        }
    }
}