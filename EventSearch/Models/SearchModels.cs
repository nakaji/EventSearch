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
        public string Keyword { get; set; }

        [Display(Name = "年")]
        public int Year { get; set; }

        [Display(Name = "月")]
        public int Month { get; set; }

        public List<CommonEvent> Events { get; set; } 
    }
}