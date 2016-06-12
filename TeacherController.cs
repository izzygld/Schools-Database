using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HoneyMustard.Web.Models;
using HoneyMustard.Interface.DataSource;
using HoneyMustard.Interface.Models;
using HoneyMustard.Factories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HoneyMustard.Web.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        public TeacherController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        // GET: Teacher
        public ActionResult ClassList()
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                
                ClassListModel model = new ClassListModel
                {
                    TeacherName = ds.Contacts.SingleOrDefault(x=>x.ContactID==user.ContactID).FirstMidName +" "+ ds.Contacts.SingleOrDefault(x=>x.ContactID==user.ContactID).LastName,
                    Classes = ds.Courses.Where(x=>x.ContactID==user.ContactID)
                                        .Select(x => new ClassListTableRow
                    {
                        CourseID = x.CoursesID,
                        CourseName = x.CourseName,
                        Module = x.Module,
                        StartDate = x.StartDate
                    }).ToList()
                };

                return View(model);
            }
        }

        public ActionResult GradeClass(int id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                var course = ds.Courses.SingleOrDefault(x => x.CoursesID == id);
                GradeClassModel model = new GradeClassModel
                {
                    CourseID = id,
                    CourseName = course.CourseName,
                    Module = course.Module,
                    StartDate = course.StartDate,
                    Participants = ds.Participant.Where(x => x.CoursesID == id).Select(x => new ParticipantModel
                    {
                        ParticpantID = x.ParticipantID,
                        FirstName = x.Contact.FirstMidName,
                        LastName = x.Contact.LastName,
                        Grade = x.Grade
                    }).ToList()

                };
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult GradeClassEdit(int id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                var course = ds.Courses.SingleOrDefault(x => x.CoursesID == id);
                GradeClassModel model = new GradeClassModel
                {
                    CourseID = id,
                    CourseName = course.CourseName,
                    Module = course.Module,
                    StartDate = course.StartDate,
                    Participants = ds.Participant.Where(x => x.CoursesID == id).Select(x => new ParticipantModel
                    {
                        ParticpantID = x.ParticipantID,
                        FirstName = x.Contact.FirstMidName,
                        LastName = x.Contact.LastName,
                        Grade = x.Grade
                    }).ToList()

                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult GradeClassEdit(int id, GradeClassModel model)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                if(ModelState.IsValid)
                {
                    foreach (ParticipantModel participant in model.Participants)
                    {
                    ds.Participant.Single(x => x.ParticipantID == participant.ParticpantID).Grade = participant.Grade;
                    ds.SaveChanges();
                    }

                    return RedirectToAction("GradeClass/" + id);
                }
                else
                {
                    return RedirectToAction("GradeClassEdit/" + id);
                }

            }
        }

        public ActionResult StudentGradeDetails(int id)
        {
            using (IHoneyMustardDataSource ds = DataSourceFactory.GetDataSource())
            {
                var student = ds.Participant.SingleOrDefault(x => x.ParticipantID == id);
                StudentGradeDetailsModel model = new StudentGradeDetailsModel
                {
                    FirstName = student.Contact.FirstMidName,
                    LastName = student.Contact.LastName,
                    Grades = ds.Participant.Where(x => x.ContactID == student.ContactID).Select(x => new GradeTableModel
                    {
                        Coursename = x.Courses.CourseName,
                        Module = x.Courses.Module,
                        Grade = x.Grade
                    }).ToList()
                };
                return View(model);
            }
        }
    }
}