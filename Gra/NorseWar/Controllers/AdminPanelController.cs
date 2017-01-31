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
        // GET: AdminPanel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Items()
        {
            //List<ItemViewModel> items = new List<ItemViewModel>();
            //items.Add();
            ItemViewModel items = new ItemViewModel();
            var weapons = db.ItemWeapons.ToList();
            foreach (var weapon in weapons)
            {
                items.weapons.Add(weapon);
            }
            var shields = db.ItemShields.ToList();
            foreach (var shield in shields)
            {
                items.shields.Add(shield);
            }

            return View(items);
        }

        public ActionResult ChangeNickname()
        {
            return View();
        }
    }
}
