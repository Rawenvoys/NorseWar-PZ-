using NorseWar.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorseWar.Models;
using NorseWar.Helper;

namespace NorseWar.Controllers
{
    public class RankController : Controller
    {
        private GameContext db = new GameContext();
        public ActionResult Index()
        {
            using (db)
            {
                try
                {
                    List<Account> acc = db.Accounts.OrderByDescending(x => x.Experience).ToList();
                    return View(acc);
                }
                catch
                {
                    List<Account> acc = new List<Account>();
                    return View(acc);
                }
            }    
        }


        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchUser searchUser)
        {
            Account acc = SearchUser.Search(searchUser);

            if (acc == null)
                ViewBag.Count = "Null";

            return View("ShowThisUser", acc);
        }
    }
}