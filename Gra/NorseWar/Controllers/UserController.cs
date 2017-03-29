using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorseWar.Models;
using NorseWar.Models.DAL;
using NorseWar.Helper;

namespace NorseWar.Controllers
{
    public class UserController : Controller
    {
        private GameContext db = new GameContext();

        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.Stats);
            return View(accounts.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        public ActionResult Tavern()
        {
            var user = (Account)Session["User"];
            return View(Methods.ShowQuestions(user.AccountID));
        }


        public ActionResult Arena(int id)
        {
            //znajdz ziomka o tym id oraz staty o tym id

            var user = (Account)Session["User"];

            var list = Methods.InitializeBattle(user.AccountID, id);

            return View(list);
        }


        public ActionResult Guard()
        {
            return View();
        }

        public ActionResult GuardStarts(int id)
        {
            Methods.StartGuard(id);
            return View("Guard");
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
