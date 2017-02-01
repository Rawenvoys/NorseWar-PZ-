using NorseWar.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorseWar.Models;

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
                    List<Account> accounts = db.Accounts.OrderByDescending(x => x.Experience).ToList();
                    return View(accounts);
                }
                catch
                {
                    List<Account> accounts = new List<Account>();
                    return View(accounts);
                }
            }    
        }
    }
}