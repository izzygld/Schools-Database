using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Sql;

using System.Net;
using System.Web;
using System.Web.Mvc;
using HoneyMustard.Interface.Models;
using HoneyMustard.Web.Models;
using HoneyMustard.Interface.DataSource;
using HoneyMustard.Factories;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HoneyMustard.Web.Controllers
{
    public class ContactController : Controller
    {

        [HttpPost]
        public ActionResult ContactTypesList()
        {
            using (IHoneyMustardDataSource ds = HoneyMustard.Factories.DataSourceFactory.GetDataSource())
            {
                var contactType = ds.ContactType.Select(x => new ContactTypeModel
                {
                    
                    ContactTypeID = x.ContactTypeID,
                    TypeDescription = x.TypeDescription,
                });

                return Json(contactType.ToList());
            }
        }


        [HttpPost]
        public ActionResult ContactList()
        {
            using (IHoneyMustardDataSource ds = HoneyMustard.Factories.DataSourceFactory.GetDataSource())
            {
                var contacts = ds.Contacts.Select(x => new ContactListTableRow
                {
                    ContactID = x.ContactID,
                    FirstMidName = x.FirstMidName + " " + x.LastName,

                });

                return Json(contacts.ToList());
            }
        }





        [HttpPost]
        public ActionResult ReminderContactList()
        {
            using (IHoneyMustardDataSource ds = HoneyMustard.Factories.DataSourceFactory.GetDataSource())
            {
                var contacts = ds.Contacts.Select(x => new FromReminderListTableRow
                {
                    ContactID = x.ContactID,

                    FirstMidName = x.FirstMidName + " " + x.LastName + "[ID  =  " + x.TeudatZehutPassport + "]",

                });

                return Json(contacts.ToList());
            }
        }
      

        public ActionResult ReminderDetails(int id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                var reminder = ds.Reminder.SingleOrDefault(x => x.ReminderID == id);

                ReminderCenter model = new ReminderCenter
                {
                    ReminderID = reminder.ReminderID,

                    Subject = reminder.Subject,
                    Details = reminder.Details,
                    ReminderDateTime = reminder.AlertDateTime,

                    From = ds.Contacts.Where(x => x.ContactID == reminder.From)
                    .Select(x => new FromReminderListTableRow
                    {

                        ContactID = x.ContactID,
                        FirstMidName = x.FirstMidName,
                        LastName = x.LastName
                    }).ToList(),

                    To = ds.Contacts.Where(x => x.ContactID == reminder.To)
                    .Select(x => new ToReminderListTableRow
                    {

                        ContactID = x.ContactID,
                        FirstMidName = x.FirstMidName,
                        LastName = x.LastName
                    }).ToList()


                };

                
                return View(model);
            }

        }



        public ActionResult Options()
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                return View();
            }
        }


        
        public ActionResult CreateReminder()
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReminder([Bind(Include = "ReminderID,From,To,Subject,Details,AlertDateTime")] Reminder reminder)
        {

            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                if (ModelState.IsValid)
                {
                    ds.Reminder.Add(reminder);
                    ds.SaveChanges();
                    return RedirectToAction("DisplayReminderList");
                }

                return View(reminder);
            }
        }

        public ActionResult DeleteReminder(int? id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reminder reminder = ds.Reminder.Find(id);
                if (reminder == null)
                {
                    return HttpNotFound();
                }
                return View(reminder);
            }
        }
        // POST: Courses/Delete/5
        [HttpPost, ActionName("DeleteReminder")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReminderConfirmed(int id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                Reminder reminder = ds.Reminder.Find(id);
                ds.Reminder.Remove(reminder);
                ds.SaveChanges();
                return RedirectToAction("DisplayReminderList");
            }
        }

        [HttpGet]
        public ActionResult ContactEdit( int id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                var contact = ds.Contacts.SingleOrDefault(x => x.ContactID == id);



                ContactCenterModel model = new ContactCenterModel
                {
                    ContactID = id,
                    FirstMidName = contact.FirstMidName,
                    LastName = contact.LastName,
                    EmailAddress = contact.EmailAddress,
                    CountryID = contact.CountryID,
                    CellPhone = contact.CellPhone,
                    Phone = contact.Phone,
                    TeudatZehutPassport = contact.TeudatZehutPassport,
                    Address = contact.Address,

                    contactType =
                    ds.ContactType
                    .Where(x => x.ContactTypeID == contact.ContactType)
                  .Select(x => new ContactTypeModel
                   {

                       ContactTypeID = x.ContactTypeID,
                       TypeDescription = x.TypeDescription,

                       //   FirstMidName = x.FirstMidName + " " + x.LastName,

                       // ContactTypeID = int.Parse(x.TypeDescription),

                   }).ToList()

                };

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ContactEdit(int id, ContactCenterModel model)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {

                if (ModelState.IsValid)
                {

                  
                    foreach (ContactTypeModel contactType in model.contactType)
                    {
                        ds.ContactType.Single(x => x.ContactTypeID == contactType.ContactTypeID);
                        // .TypeDescription = contactType.TypeDescription;

                        ds.SaveChanges();
                    }

                    return RedirectToAction("DisplayContactList");

                }


                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));


                    return
                        RedirectToAction("Options/" + id);
                }
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
        }

        //[HttpGet]
        //public ActionResult ContactEdit(int id)
        //{
        //    using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
        //    {
        //        var contact = ds.Contacts.SingleOrDefault(x => x.ContactID == id);



        //        ContactEdit model = new ContactEdit
        //        {
        //            ContactID = id,
        //            FirstMidName = contact.FirstMidName,
        //            LastName = contact.LastName,
        //            EmailAddress = contact.EmailAddress,
        //            CountryID = contact.CountryID,
        //            CellPhone = contact.CellPhone,
        //            Phone = contact.Phone,
        //            TeudatZehutPassport = contact.TeudatZehutPassport,
        //            Address = contact.Address,

        //            contactType = contact.ContactType


        //        };

               
            
        //        return View(model);
        //    }
        //}

        //[HttpPost]
        //public ActionResult ContactEdit(int id, ContactEdit model)
        //{
        //    using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
        //    {

        //        if (ModelState.IsValid)
        //        {

        //            //ds.Entry(model).State = EntityState.Modified;
        //           // ds.Contacts.SubmitChanges(model);

        //                ds.SaveChanges();
        //             return RedirectToAction("Index");
        //            }


                


        //        else
        //        {
        //            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));

        //            ViewBag.Error = TempData["error Please try again"];


        //            return
        //                RedirectToAction("ContactEdit/" + id);
        //        }
        //        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    }
        //}












































        //public ActionResult EditReminder(int? id)
        //{
        //    using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
        //    {

        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }

        //        Reminder reminder = ds.Reminder.Find(id);

        //        if (reminder == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        //ViewBag.ContactID = new SelectList(ds.Contacts, "ContactID", "FirstMidName", reminder.ReminderID);
        //        return View(reminder);
        //    }

        //}


    [HttpGet]
        public ActionResult EditReminder(int id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {

                var reminder = ds.Reminder.SingleOrDefault(x=>x.ReminderID == id);
                EditReminderModel model = new EditReminderModel
                {
                    ReminderID = id,
                    From = reminder.From,
                    To = reminder.To,
                    Subject = reminder.Subject,
                    Details = reminder.Details,
                    ReminderDateTime = reminder.AlertDateTime

                };
                return View(model);
            }

        }



       [HttpPost]
       public ActionResult EditReminder (int id, EditReminderModel model )
    {
                       using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
                       {
                           if (ModelState.IsValid)
                           {
                               ds.SaveChanges();
                           }
                           return RedirectToAction("DisplayReminderList");
                       }

    }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditReminder([Bind(Include = "FirstMidName,LastName,EmailAddress,Address,TeudatZehutPassport,CountryID,Phone,CellPhone")] Reminder reminder)
        //{
        //    using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //ds.Entry(course).State = EntityState.Modified;
        //            ds.SaveChanges();
        //            return RedirectToAction("DisplayReminderList");
        //        }
        //        //ViewBag.ContactID = new SelectList(ds.Contacts, "ContactID", "FirstMidName", contact.ContactID);
        //        return View(reminder);
        //    }
        //}



        
                
         
        





        public ActionResult ReminderList()
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {

                ReminderScreen model = new ReminderScreen
                {                                  
                     Reminders = ds.Reminder.Include(c=>c.From)
                     .Select(x=> new ReminderCenter 
                                                                       
                      {

                            ReminderID = x.ReminderID,
        

                    Subject = x.Subject,
                    Details = x.Details,
                    ReminderDateTime = x.AlertDateTime,




                    //        From = ds.Reminder.Where(s => s. )
                    //.Select(d => new FromReminderListTableRow
                    //{
                        
                    //})

                   
                    
                  

                    }).ToList()

                    //ContactID = x.ContactID,
                    //FirstMidName = x.FirstMidName + " " + x.LastName,

                    //To = ds.Contacts.Where(x => x.ContactID == reminder.To)
                    //.Select(x => new ToReminderListTableRow
                    //{

                    //    ContactID = x.ContactID,
                    //    FirstMidName = x.FirstMidName,
                    //    LastName = x.LastName
                    //}).ToList()


                };

                return View(model);
            }
        }

        public ActionResult DisplayReminderList()
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                ReminderListCenter model = new ReminderListCenter
                {
                    Reminderss = ds.Reminder.Select(x => new ReminderList
                    {
                        
                        ReminderID = x.ReminderID,
                        From = ds.Contacts.FirstOrDefault(d => d.ContactID == x.From).FirstMidName + " " + ds.Contacts.FirstOrDefault(d => d.ContactID == x.From).LastName,
                        To = ds.Contacts.FirstOrDefault(d => d.ContactID == x.To).FirstMidName + " " + ds.Contacts.FirstOrDefault(d => d.ContactID == x.To).LastName,

                        Details = x.Details,
                        ReminderDateTime = x.AlertDateTime,
                        Subject = x.Subject,

                    }
                    ).ToList()

                };
                return View(model);

            }
        }

        public ActionResult DisplayContactList()
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                contactDisplay model = new contactDisplay
                {
                    Contactss = ds.Contacts.Select(x=> new ContactListDisplay
                    {
                        ContactID = x.ContactID,
                        FirstMidName = x.FirstMidName,
                        LastName = x.LastName,
                        Address = x.Address,
                        EmailAddress = x.EmailAddress,
                        Phone = x.Phone,
                        CellPhone = x.CellPhone,
                        TeudatZehutPassport = x.TeudatZehutPassport,
                        CountryID = x.CountryID,
                        contactType = ds.ContactType.FirstOrDefault(d => d.ContactTypeID == x.ContactType).TypeDescription,
                    }
                    ).ToList()
                };
                return View(model); 
            }
        }

        // GET: Contact
        public ActionResult Index()
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {

                ContactModel model = new ContactModel
                {

                    Contacts = ds.Contacts
                    //.Include(c => c.ContactType)
                    .Select(x=> new ContactCenterModel
                    {
                        ContactID = x.ContactID,
                        FirstMidName = x.FirstMidName,
                        LastName = x.LastName,
                        EmailAddress = x.EmailAddress,
                        Address = x.Address,
                        TeudatZehutPassport = x.TeudatZehutPassport,
                        CountryID = x.CountryID,
                        Phone = x.Phone,
                        CellPhone = x.CellPhone,

                    }).ToList()

                };

                return View(model);
            }
        }
        

        // GET: Contact/Create
        public ActionResult Create()
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                ViewBag.ContactID = new SelectList(ds.Contacts, "ContactID", "FirstMidName");
                return View();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstMidName,LastName,EmailAddress,ContactType,Address,TeudatZehutPassport,CountryID,Phone,CellPhone")] Contact contact)
        {

            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                if (ModelState.IsValid)
                {
                    ds.Contacts.Add(contact);
                    ds.SaveChanges();
                    return RedirectToAction("DisplayContactList");
                }

                ViewBag.ContactID = new SelectList(ds.Contacts, "ContactID", "FirstMidName", contact.ContactID);
                return View(contact);
            }
        }
        



        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Contact contact = ds.Contacts.Find(id);
                if (contact == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ContactID = new SelectList(ds.Contacts, "ContactID", "FirstMidName", contact.ContactID);
                return View(contact);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirstMidName,ContactType,LastName,EmailAddress,Address,TeudatZehutPassport,CountryID,Phone,CellPhone")] Contact contact)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                if (ModelState.IsValid)
                {
                    //ds.Entry(course).State = EntityState.Modified;
                    ds.SaveChanges();
                    return RedirectToAction("DisplayContactList");
                }
                ViewBag.ContactID = new SelectList(ds.Contacts, "ContactID", "FirstMidName", contact.ContactID);
                return View(contact);
            }
        }


        // GET: contact/Details/5
        public ActionResult Details(int id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                var contact = ds.Contacts.SingleOrDefault(x=> x.ContactID ==id);
                ContactCenterModel model = new ContactCenterModel
                {
                    ContactID = contact.ContactID,
                    FirstMidName = contact.FirstMidName,
                    LastName = contact.LastName,
                    EmailAddress = contact.EmailAddress,
                    Address = contact.Address,
                    CountryID = contact.CountryID,
                    CellPhone = contact.CellPhone,
                    Phone = contact.Phone,
                    TeudatZehutPassport = contact.TeudatZehutPassport,
                    


                    contactType = ds.ContactType.Where(x => x.ContactTypeID == contact.ContactType)
                    .Select(x => new ContactTypeModel
                    {
                        ContactTypeID = x.ContactTypeID,
                        TypeDescription = x.TypeDescription
                       
                    }).ToList()


                };
               
                return View(model);
            }
        }

        

        public ActionResult Delete(int? id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Contact contact = ds.Contacts.Find(id);
                if (contact == null)
                {
                    return HttpNotFound();
                }
                return View(contact);
            }
        }
        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                Contact contact = ds.Contacts.Find(id);
                ds.Contacts.Remove(contact);
                ds.SaveChanges();
                return RedirectToAction("DisplayContactList");
            }
        }
    }
}
