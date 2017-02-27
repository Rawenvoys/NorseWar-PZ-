using NorseWar.Helper;
using NorseWar.Models;
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
            if (Session["User"] == null)
                return RedirectToAction("Login", "Panel");

            else
                return View();          
        }
    }
}