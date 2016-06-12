using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HoneyMustard.Web.Models
{
    public class PhoneRecordScreen
    {
        public List<SelectListItem> Name { get; set; }
        public List<SelectListItem> Subject { get; set; }
        public List<SelectListItem> Phone { get; set; }
        public List<SelectListItem> CellPhone { get; set; }

        public int ContactID { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string CallSubject { get; set; }
        public string LandLine { get; set; }
        public string CellularPhone { get; set; }
        public string Details { get; set; }
    }
}