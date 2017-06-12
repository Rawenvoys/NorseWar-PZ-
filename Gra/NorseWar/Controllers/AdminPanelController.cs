using NorseWar.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorseWar.Models.ViewModel;

//Czyli, 
//+wypisać wszystkie itemy w grze
//-dodaj item do bazy danych
//-wypisać wszystkich graczy
//-edytuj nazwę gracza
//-pokaż info o graczu?
//-napisz @ do wsyzstkich
//-banuj

namespace NorseWar.Controllers
{
    public class AdminPanelController : Controller
    {
        private GameContext db = new GameContext();
        public ActionResult Index()
        {
            Session["User"] = null;
            return View();
        }


        [HttpPost]
        public ActionResult Index(String Login, String Password)
        {
            if (Login == "admin" && Password == "admin")
            {
                Session["Admin"] = "admin";
                return RedirectToAction("Index", "Accounts");
            }

            return View();
        }
    }
}
