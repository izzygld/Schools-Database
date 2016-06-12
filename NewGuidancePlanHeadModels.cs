using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace HoneyMustard.Web.Models
{
    public class NewGuidancePlanHeadModels
    {
        public int GuidancePlanID { get; set; }

        public int TeudatZehutPassport { get; set; }
        public string StudentName { get; set; }
        public DateTime DateCreated { get; set; }

        [Required]
        public List<SelectListItem> Degrees { get; set; }

        [Required]
        public List<SelectListItem> PeriodFrom { get; set; }
        [Required]
        public int YearFrom { get; set; }

        [Required]
        public List<SelectListItem> PeriodEnd { get; set; }
        [Required]
        public int YearEnd { get; set; }

        [Required]
        public List<SelectListItem> Authors { get; set; }
    }

    public class Degree
    {
        public string DegreeName { get; set; }
        public double? TotalCredit { get; set; }
    }

    public class Author
    {
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
    }

    public class ContactComboBoxRowModel
    {
        [Required]
        public int ContactID { get; set; }
        public string Name { get; set; }
        public int TeudatZehutPassport { get; set; }     
    }

    public class AuthorComboBoxRowModel
    {
        [Required]
        public int ContactID { get; set; }
        public string Name { get; set; }
    }

    public class DegreeComboBoxRowModel
    {
        public int DegreeID { get; set; }
        public string Name { get; set; }
        public double? TotalCredit { get; set; }
    }

    public class CoursesComboBoxRowModel
    {
        public int CoursesID { get; set; }
        public string CourseName { get; set; }
        public double? Credit { get; set; }
    }

    public class NewGuidancePlanHeadPostModel
    {
        public int GuidancePlanID { get; set; }

        [Required]
        public int DegreeID { get; set; }

        [Required]
        public int PeriodFrom { get; set; }
        [Required]
        public int YearFrom { get; set; }

        [Required]
        public int PeriodEnd { get; set; }
        [Required]
        public int YearEnd { get; set; }

        [Required]
        public int AuthorID { get; set; }
    }
}
