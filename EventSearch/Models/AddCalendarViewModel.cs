using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using EventData;

namespace EventSearch.Models
{
    public class AddCalendarViewModel
    {
        public CommonEvent @Event { get; set; }
        [DisplayName("カレンダー")]
        public List<CalendarInfo> CalendarList { get; set; }
        public string CalendarId { get; set; }
        public int TimeZoneOffset { get; set; }
    }
}