using System.Collections.Generic;
using System.Web.Mvc;

namespace HoneyMustard.Web.Models
{
    public class FinanceSearchModels
    {
        public List<SelectListItem> Students { get; set; }

        public List<SelectListItem> Employees { get; set; }
    }
}