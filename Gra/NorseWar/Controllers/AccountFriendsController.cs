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

namespace NorseWar.Controllers
{
    public class AccountFriendsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: AccountFriends
        public ActionResult Index()
        {
            var accountFriends = db.AccountFriends.Include(a => a.Account).Include(a => a.Friend);
            return View(accountFriends.ToList());
        }

        // GET: AccountFriends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountFriend accountFriend = db.AccountFriends.Find(id);
            if (accountFriend == null)
            {
                return HttpNotFound();
            }
            return View(accountFriend);
        }

        // GET: AccountFriends/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login");
            ViewBag.FriendID = new SelectList(db.Friends, "FriendID", "Name");
            return View();
        }

        // POST: AccountFriends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountFriendID,AccountID,FriendID")] AccountFriend accountFriend)
        {
            if (ModelState.IsValid)
            {
                db.AccountFriends.Add(accountFriend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountFriend.AccountID);
            ViewBag.FriendID = new SelectList(db.Friends, "FriendID", "Name", accountFriend.FriendID);
            return View(accountFriend);
        }

        // GET: AccountFriends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountFriend accountFriend = db.AccountFriends.Find(id);
            if (accountFriend == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountFriend.AccountID);
            ViewBag.FriendID = new SelectList(db.Friends, "FriendID", "Name", accountFriend.FriendID);
            return View(accountFriend);
        }

        // POST: AccountFriends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountFriendID,AccountID,FriendID")] AccountFriend accountFriend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountFriend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountFriend.AccountID);
            ViewBag.FriendID = new SelectList(db.Friends, "FriendID", "Name", accountFriend.FriendID);
            return View(accountFriend);
        }

        // GET: AccountFriends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountFriend accountFriend = db.AccountFriends.Find(id);
            if (accountFriend == null)
            {
                return HttpNotFound();
            }
            return View(accountFriend);
        }

        // POST: AccountFriends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountFriend accountFriend = db.AccountFriends.Find(id);
            db.AccountFriends.Remove(accountFriend);
            db.SaveChanges();
            return RedirectToAction("Index");
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
