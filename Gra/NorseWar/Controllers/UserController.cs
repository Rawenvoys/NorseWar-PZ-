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
            StatsBoost boots = db.StatsBoosts.Single(x => x.AccountId == account.AccountID);
            AccountAndBoost accountBoost = new AccountAndBoost() { Account = account, StatsBoost = boots }; 

            if (account == null)
            {
                return HttpNotFound();
            }
            return View(accountBoost);
        }

        public ActionResult Tavern()
        {
            var user = (Account)Session["User"];
            Methods.AddStatsBonus(user);
        //    Methods.AddItemsToToNewUser(user);
            return View(Methods.ShowQuestions(user.AccountID));
        }


        public ActionResult Market()
        {
            var user = (Account)Session["User"];
            return View(Methods.SetItemToMarket(user));
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


        public void MissionCancels()
        {
            var user = (Account)Session["User"];
            Methods.CancelTavern(user);
        }


        public JsonResult BuyItemFromMarket(int id)
        {
            var user = (Account)Session["User"];
            var item = Methods.BuyItemFromMarket(user, id);
            return Json(item);
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


        public JsonResult QuestEndTime()
        {
            var user = (Account)Session["User"];
            var result = Methods.ShowQuestEndTime(user);
            var start = result[0];
            var now = result[1];
            var end = result[2];
            var list = new List<object>();
            list.Add(new { start = result[0], now = result[1], end = result[2] });

            return Json(list);
        }


        public void FinishQuest()
        {
            var user = (Account)Session["User"];
            Methods.QuestFinish(user);
        }


        public int SetPoints(int id)
        {
            var user = (Account)Session["User"];
            var data = Methods.AddPoint(id, user);
            return data;
        }

        public int getBoost(string name)
        {
            var user = (Account)Session["User"];
            var data = Methods.getBoost(name, user);
            return data;
        }

        public void SelectQuest(int id)
        {
            var user = (Account)Session["User"];
            Methods.SelectOneQuest(user, id);
        }


        public void DragItemToFreeSpace(int id)
        {
            var user = (Account)Session["User"];
            Methods.DragItem(user, id);
        }


        public void TakeOffItem(int id)
        {
            var user = (Account)Session["User"];
            Methods.TakeOffEquippedItem(user, id);
        }


        public void ChangeItems(int on, int off)
        {
            var user = (Account)Session["User"];
            Methods.ChangeItems(user, on, off);
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
