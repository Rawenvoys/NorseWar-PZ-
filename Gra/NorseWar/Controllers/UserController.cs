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


        public ActionResult Arena()
        {
            var user = (Account)Session["User"];
            var list = Methods.Arena3Players(user);
            return View(list);
        }

        public ActionResult Fight(int id)
        {
            var user = (Account)Session["User"];
            var list = Methods.InitializeBattle(user.AccountID, id);
            return View(list);
        }


        public ActionResult Guard()
        {
            return View();
        }


        public void GuardStarts(int id)
        {
            var user = (Account)Session["User"];
            Methods.StartGuard(id, user);
        }


        public void GuardCancels()
        {
            var user = (Account)Session["User"];
            Methods.CancelGuard(user);
        }


        public JsonResult GuardEndTime()
        {
            var user = (Account)Session["User"];
            var result = Methods.ShowGuardEndTime(user);
            var start = result[0];
            var now = result[1];
            var end = result[2];
            var list = new List<object>();
            list.Add(new {start = result[0], now = result[1], end = result[2]});

            return Json(list);
        }


        public int SetPoints(int id)
        {
            var user = (Account)Session["User"];
            var data = Methods.AddPoint(id, user);
            return data;
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
