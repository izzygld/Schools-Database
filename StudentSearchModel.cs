using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoneyMustard.Web.Models
{
    public class StudentSearchModel
    {
        public int ContactID { get; set; }
        public List<SelectListItem> Students { get; set; }

    }
}