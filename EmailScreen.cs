using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoneyMustard.Web.Models
{
    public class EmailScreen
    {
        public List<SelectListItem> Contacts { get; set; }
        public List<SelectListItem> NewSubject { get; set; }
        public List<SelectListItem> CCs { get; set; }
        public List<SelectListItem> BCCs { get; set; }

        public int ContactID { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string CarbonCopies { get; set; }
        public string BlindCarbonCopies { get; set; }
        public string Details { get; set; }
        public DateTime EmailDateTime { get; set; }
    }
}