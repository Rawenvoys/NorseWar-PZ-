using NorseWar.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorseWar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           // Methods.SaveUserSession(null, null);
            if (Methods.userMail == null)
                return RedirectToAction("Login", "Panel");

            else
                return View();          
        }
    }
}