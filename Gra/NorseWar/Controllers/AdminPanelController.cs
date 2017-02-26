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
            return View();
        }


        [HttpPost]
        public ActionResult Index(String Login, String Password)
        {
            if (Login == "admin" && Password == "admin")
            {
                return RedirectToAction("Index", "Accounts");
            }

            return View();
        }


        public ActionResult Pobierz()
        {
            Random rand = new Random();
            var accList = db.Accounts.ToList();

            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

            //wybierz randoma i usun z listy

            var res = accList.OrderBy(x => rand.Next(accList.Count)).Take(3);

            var rr = list.OrderBy(x => x);
            int ok = 2;
            return View(res);
        }
    }
}
