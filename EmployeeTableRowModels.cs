using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HoneyMustard.Web.Models
{
    public class EmployeeTableRowModels
    {
        public int ContactID { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime Date { get; set; } 
    }
    public class EmployeeFinanceModel
    {
        public List<EmployeeTableRowModels> Payments { get; set; }
        public List<SelectListItem> PaymentTypes { get; set; }

        public int ContactID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public int CellPhone { get; set; }

    }
}