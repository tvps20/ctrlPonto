using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtrlPonto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.active = "Home";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.active = "Sobre";
            return View();
        }

        public ActionResult Menu(string active)
        {
            ViewBag.active = active;
            return PartialView();
        }
    }
}