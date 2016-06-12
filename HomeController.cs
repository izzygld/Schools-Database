using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoneyMustard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Our (as in; US) application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contact page.";

            return View();
        }
    }
}