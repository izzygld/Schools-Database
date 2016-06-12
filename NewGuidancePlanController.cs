using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoneyMustard.Web.Models;
using HoneyMustard.Interface.DataSource;
using HoneyMustard.Factories;
using HoneyMustard.Interface.Models;

namespace HoneyMustard.Web.Controllers
{
    public class NewGuidancePlanController : Controller
    {
        // GET: NewGuidancePlan
        public ActionResult GuidancePlan(int id)
        {
            using (IHoneyMustardDataSource ds = HoneyMustard.Factories.DataSourceFactory.GetDataSource())
            {
                GuidancePlan gp = ds.GuidancePlan.Create();
                gp.StudentID = id;
                gp.DateCreated = DateTime.Now;
                gp.IsSaved = false;
                gp.AuthorID = 1; //This is No Author in database. Number needs to change depending on ContctID for this name. 
                gp.DegreeID = 1; //This is None in database. Number needs to change depending on DegreeID for this name.
                gp.PeriodStartYear = 2000;
                gp.PeriodEndYear = 2000;
                ds.GuidancePlan.Add(gp);
                ds.SaveChanges();
                
                NewGuidancePlanHeadModels model = new NewGuidancePlanHeadModels
                {
                    TeudatZehutPassport = gp.Student.TeudatZehutPassport,
                    StudentName = gp.Student.LastName + ", " + gp.Student.FirstMidName,
                    DateCreated = gp.DateCreated,
                    GuidancePlanID = gp.GuidancePlanID
                };

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ContactList() {
            using (IHoneyMustardDataSource ds = HoneyMustard.Factories.DataSourceFactory.GetDataSource())
            {
                var contacts = ds.ContactJoin.Where(x => x.ContactType.TypeDescription == "Student").Select(x => new ContactComboBoxRowModel
                {
                    ContactID = x.ContactID,
                    TeudatZehutPassport = x.Contact.TeudatZehutPassport,
                    Name = x.Contact.LastName + ", " + x.Contact.FirstMidName,
                });

                return Json(contacts.ToList());
            }
        }

        [HttpPost]
        public ActionResult AuthorList()
        {
            using (IHoneyMustardDataSource ds = HoneyMustard.Factories.DataSourceFactory.GetDataSource())
            {
                var contacts = ds.ContactJoin.Where(x => x.ContactType.TypeDescription == "Administrator" || x.ContactType.TypeDescription == "Teacher").Select(x => new AuthorComboBoxRowModel
                {
                    ContactID = x.ContactID,
                    Name = x.Contact.LastName + ", " + x.Contact.FirstMidName,
                });

                return Json(contacts.ToList());
            }
        }

        [HttpPost]
        public ActionResult DegreeList()
        {
            using (IHoneyMustardDataSource ds = HoneyMustard.Factories.DataSourceFactory.GetDataSource())
            {
                var degrees = ds.Degree.Select(x => new DegreeComboBoxRowModel
                {
                    DegreeID = x.DegreeID,
                    Name = x.Name,
                    TotalCredit = x.TotalCredits
                });

                return Json(degrees.ToList());
            }
        }

        [HttpPost]
        public ActionResult CoursesList()
        {
            using (IHoneyMustardDataSource ds = HoneyMustard.Factories.DataSourceFactory.GetDataSource())
            {
                var degrees = ds.Courses.Select(x => new CoursesComboBoxRowModel
                {
                    CoursesID = x.CoursesID,
                    CourseName = x.CourseName,
                    Credit = x.Credit
                });

                return Json(degrees.ToList());
            }
        }

        [HttpPost]
        public ActionResult GuidancePlan( NewGuidancePlanHeadPostModel model)
        {
            using (IHoneyMustardDataSource ds = HoneyMustard.Factories.DataSourceFactory.GetDataSource())
            {
                if (ModelState.IsValid)
                {
                    GuidancePlan gp = ds.GuidancePlan.SingleOrDefault(x => x.GuidancePlanID == model.GuidancePlanID);
                    gp.IsSaved = true;
                    gp.AuthorID = model.AuthorID;
                    gp.DegreeID = model.DegreeID;
                    gp.PeriodStart = model.PeriodFrom;
                    gp.PeriodStartYear = model.YearFrom;
                    gp.PeriodEnd = model.PeriodEnd;
                    gp.PeriodEndYear = model.YearEnd;
                    ds.GuidancePlan.Add(gp);
                    ds.SaveChanges();
                }
                else
                {
                    Response.Write("modelstate not valid");
                    Response.End();
                }
                
                return RedirectToAction("GuidancePlan","NewGuidancePlan");
            }
        }
    }
}