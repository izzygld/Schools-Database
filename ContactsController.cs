using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoneyMustard.Interface.DataSource;
using HoneyMustard.Web.Models;
using HoneyMustard.Factories;
using HoneyMustard.Interface.Models;

namespace HoneyMustard.Web.Controllers
{
    public class ContactsController : Controller
    {
        // GET: Contacts
        public ActionResult ContactsScreens()
        {
            return View();
        }

        public ActionResult PhoneRecordScreen()
        {
            return View();
        }

        public ActionResult EmailScreen()
        {
            return View();
        }

        public ActionResult SearchCallsScreen()
        {
            return View();
        }

        public ActionResult ReminderScreen()
        {
            return View();
        }
/*        public ActionResult PhoneRecordScreen() 
          {
            return Json(ds.Contacts.Select(x => new ContactComboBoxRowModel {
            ContactID = x.ContactID,
            Name = x.FirstName
          }); 
 */
        [HttpPost]
        public ActionResult ContactList()
        {
            using (IHoneyMustardDataSource ds = HoneyMustard.Factories.DataSourceFactory.GetDataSource())
            {
                var contacts = ds.Contacts.Select(x => new ContactComboBoxRowModel
                {
                    ContactID = x.ContactID,
                    Name = x.FirstMidName + " " + x.LastName
                });

                return Json(contacts.ToList());
            }
        }
    }

}
