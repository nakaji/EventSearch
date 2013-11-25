using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EventData;

namespace EventSearch.Models
{
    public class SearchModels
    {
        [Display(Name = "キーワード")]
        [Required]
        public string Keyword { get; set; }

        [Display(Name = "年")]
        [Range(2013, 2020)]
        public int Year { get; set; }

        [Display(Name = "月")]
        [Range(1,12)]
        public int Month { get; set; }

        public int TimeZoneOffset { get; set; }

        public List<CommonEvent> Events { get; set; }

        public SearchModels()
        {
            Events = new List<CommonEvent>();
        }
    }
}