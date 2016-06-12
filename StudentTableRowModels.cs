using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HoneyMustard.Web.Models
{
    public class StudentTableRowModels
    {
        public int ContactID { get; set; }
        public string CourseName { get; set; }
        public decimal AgreedPrice { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime Date { get; set; }

    }

    public class StudentFinanceModel
    {
        public List<StudentTableRowModels> Payments { get; set; }
        public List<SelectListItem> Courses { get; set; }
        public List<SelectListItem> PaymentMethod { get; set; }

        public int ContactID { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public int CellPhone { get; set; }

    }
}