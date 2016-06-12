using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HoneyMustard.Web.Models
{
    public class SearchCallsScreen
    {
        public List<SelectListItem> Contacts { get; set; }
        public List<SelectListItem> PreviouslyUsedSubjects { get; set; }
        public List<SelectListItem> Phones { get; set; }
        public List<SelectListItem> CellPhones { get; set; }
       
        public int ContactID { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public DateTime AlertDateTime { get; set; }
    }
}