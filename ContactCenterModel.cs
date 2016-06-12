using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace HoneyMustard.Web.Models
{
    public class ContactModel
    {
        public List<ContactCenterModel> Contacts { get; set; }
    }
    public class ContactCenterModel
    {
        public int ContactID { get; set; }

                [Required]
        public List<ContactTypeModel> contactType { get; set; }



       
        [StringLength(100)]
        [DisplayName("First & Middle Name")]
        [Required(ErrorMessage = "Enter Contacts First & Middle Name")]
        public string FirstMidName { get; set; }

       
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Enter Contacts Last Name")]
        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Address { get; set; }
      
        [Range(6,12)]
        [DisplayName("Tehudat zehut Or passport no.")]
        [Required(ErrorMessage = "Please enter a tehudat zehut/ passport no. ")]
        public int TeudatZehutPassport { get; set; }


        public string CountryID { get; set; }

                [Required]
        public int Phone { get; set; }
                [Required]
        public int CellPhone { get; set; }

    }

    public class ContactTypeModel
    {
        public int ContactTypeID { get; set; }

        public string TypeDescription { get; set; }

    }

    public class contactDisplay
    {
        public List<ContactListDisplay> Contactss { get; set; }

    }

    public class ContactListDisplay
    {
        public int ContactID { get; set; }

        public string contactType { get; set; }




        public string FirstMidName { get; set; }


        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Address { get; set; }

        public int TeudatZehutPassport { get; set; }


        public string CountryID { get; set; }

        public int Phone { get; set; }
        public int CellPhone { get; set; }


    }



}