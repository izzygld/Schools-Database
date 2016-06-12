using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoneyMustard.Web.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        //Get: GuidanceSplashPage
        [HttpGet]
        public ActionResult GuidanceSplashPage()
        {
            return View();
        }


    }
}
