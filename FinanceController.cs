using HoneyMustard.Interface.DataSource;
using System.Linq;
using System.Web.Mvc;
using HoneyMustard.Web.Models;
using HoneyMustard.Interface.Models;
using System.Web;
using HoneyMustard.Factories;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace HoneyMustard.Web.Controllers
{
    public class FinanceController : Controller
    {
        // GET: Finance
        public ActionResult SelectContact()
        {
            using (IHoneyMustardDataSource ds = Factories.DataSourceFactory.GetDataSource())
            {
                FinanceSearchModels model = new FinanceSearchModels();
                model.Students = (from student in ds.Participant
                                  select new SelectListItem
                                  {
                                      Value = student.ContactID.ToString(),
                                      Text = student.Contact.FirstMidName + " " + student.Contact.LastName
                                  }).ToList();
                model.Employees = (from employee in ds.ContactJoin
                                   where employee.ContactType.TypeDescription=="Administrator" || employee.ContactType.TypeDescription == "Teacher"
                                   select new SelectListItem
                                   {
                                       Value = employee.ContactID.ToString(),
                                       Text = employee.Contact.FirstMidName + " " + employee.Contact.LastName
                                   }).ToList();

                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Student(StudentTableRowModels model)
        {
            using (IHoneyMustardDataSource ds = Factories.DataSourceFactory.GetDataSource())
            {
                if (ModelState.IsValid)
                {
                    ReceivePayment payment = new ReceivePayment();
                    payment.AmountPaid = model.AmountPaid;
                    payment.Date = model.Date;
                    payment.PaymentMethodID = int.Parse(model.PaymentMethod);
                    payment.CoursesID = int.Parse(model.CourseName);
                    payment.ParticipantID = ds.Participant.SingleOrDefault(x => x.ContactID == model.ContactID).ParticipantID;
                    ds.ReceivePayment.Add(payment);
                    ds.SaveChanges();
                }
                return RedirectToAction ("Student",new { id = model.ContactID });
            }
        }

        // Get: /Finance/Student/ 

        public ActionResult Student(int id)
        {
            using (IHoneyMustardDataSource ds = Factories.DataSourceFactory.GetDataSource())
            {
                StudentFinanceModel model = (from student in ds.Contacts
                                             where student.ContactID == id
                                             select new StudentFinanceModel
                                             {
                                                 Name = student.FirstMidName + " " + student.LastName,
                                                 Address = student.Address,
                                                 EmailAddress = student.EmailAddress,
                                                 Phone = student.Phone,
                                                 CellPhone = student.CellPhone,
                                                 ContactID = student.ContactID
                                             }).SingleOrDefault();
                {
                    model.Payments = ds.ReceivePayment.Where(x => x.Participant.ContactID == id)
                          .Select(x => new StudentTableRowModels
                          {
                              CourseName = x.Courses.CourseName,
                              AgreedPrice = x.Participant.AgreedPrice,
                              AmountPaid = x.AmountPaid,
                              PaymentMethod= x.PaymentMethod.Method,
                              Date= x.Date
                          }).ToList();
                };

                model.Courses = (from Participant in ds.Participant
                                 where Participant.ContactID == id
                                 select new SelectListItem
                                 {
                                     Value = Participant.CoursesID.ToString(),
                                     Text = Participant.Courses.CourseName
                                 }).ToList();
                model.PaymentMethod = (from paymentMethod in ds.PaymentMethod
                                       select new SelectListItem
                                       {
                                           Value = paymentMethod.PaymentMethodID.ToString(),
                                           Text = paymentMethod.Method
                                       }).ToList();
                decimal totalAgreedPrice = ds.Participant.Where(x => x.ContactID == id).Sum(x => x.AgreedPrice);
                decimal totatPaid = model.Payments.Sum(x => x.AmountPaid);
                model.Balance = totalAgreedPrice - totatPaid;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Employee(EmployeeTableRowModels model)
        {
            using (IHoneyMustardDataSource ds = Factories.DataSourceFactory.GetDataSource())
            {
                if (ModelState.IsValid)
                {
                    PayBill payment = new PayBill();

                    payment.AmountPaid = model.AmountPaid;
                    payment.Date = model.Date;
                    payment.PaymentMethodID = int.Parse(model.PaymentMethod);
                    payment.ContactID = model.ContactID;
                    ds.PayBill.Add(payment);
                    ds.SaveChanges();
                }
                return RedirectToAction("Employee");
            }
        }
        // Post: /Finance/Employee/id

        public ActionResult Employee(int id)
        {
            using (IHoneyMustardDataSource ds = Factories.DataSourceFactory.GetDataSource())
            {
                EmployeeFinanceModel model = (from employee in ds.Contacts
                                              where employee.ContactID == id
                                              select new EmployeeFinanceModel
                                              {
                                                  Name = employee.FirstMidName + " " + employee.LastName,
                                                  Address = employee.Address,
                                                  EmailAddress = employee.EmailAddress,
                                                  Phone = employee.Phone,
                                                  CellPhone = employee.CellPhone,
                                                  ContactID = employee.ContactID
                                              }).SingleOrDefault();
                {
                    model.Payments = ds.PayBill.Where(x => x.Contact.ContactID == id)
                              .Select(x => new EmployeeTableRowModels
                              {
                                  PaymentMethod = x.PaymentMethod.Method,
                                  AmountPaid = x.AmountPaid,
                                  Date = x.Date,
                              }).ToList();
                    model.PaymentTypes = (from paymentMethod in ds.PaymentMethod
                                           select new SelectListItem
                                           {
                                               Value = paymentMethod.PaymentMethodID.ToString(),
                                               Text = paymentMethod.Method
                                           }).ToList();
                    return View(model);
                }
            }
        }
    }
}