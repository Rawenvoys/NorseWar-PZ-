using NorseWar.Helper;
using NorseWar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorseWar.Controllers
{
    public class BattleAreaController : Controller
    {
        // GET: BattleArea
        public ActionResult Index()
        {
            var user = (Account)Session["User"];
            Tuple<Account, Account> accounts = Methods.GetUsers(user);
            return View(accounts);
        }
    }
}