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
    public class AccountItemShieldsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: AccountItemShields1
        public ActionResult Index()
        {
            var accountItemShields = db.AccountItemShields.Include(a => a.Account).Include(a => a.ItemShield);
            return View(accountItemShields.ToList());
        }

        // GET: AccountItemShields1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountItemShield accountItemShield = db.AccountItemShields.Find(id);
            if (accountItemShield == null)
            {
                return HttpNotFound();
            }
            return View(accountItemShield);
        }

        // GET: AccountItemShields1/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login");
            ViewBag.ItemShieldID = new SelectList(db.ItemShields, "ItemShieldID", "Name");
            return View();
        }

        // POST: AccountItemShields1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountItemShieldID,ItemShieldID,AccountID,Equiped")] AccountItemShield accountItemShield)
        {
            if (ModelState.IsValid)
            {
                db.AccountItemShields.Add(accountItemShield);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountItemShield.AccountID);
            ViewBag.ItemShieldID = new SelectList(db.ItemShields, "ItemShieldID", "Name", accountItemShield.ItemShieldID);
            return View(accountItemShield);
        }

        // GET: AccountItemShields1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountItemShield accountItemShield = db.AccountItemShields.Find(id);
            if (accountItemShield == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountItemShield.AccountID);
            ViewBag.ItemShieldID = new SelectList(db.ItemShields, "ItemShieldID", "Name", accountItemShield.ItemShieldID);
            return View(accountItemShield);
        }

        // POST: AccountItemShields1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountItemShieldID,ItemShieldID,AccountID,Equiped")] AccountItemShield accountItemShield)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountItemShield).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountItemShield.AccountID);
            ViewBag.ItemShieldID = new SelectList(db.ItemShields, "ItemShieldID", "Name", accountItemShield.ItemShieldID);
            return View(accountItemShield);
        }

        // GET: AccountItemShields1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountItemShield accountItemShield = db.AccountItemShields.Find(id);
            if (accountItemShield == null)
            {
                return HttpNotFound();
            }
            return View(accountItemShield);
        }

        // POST: AccountItemShields1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountItemShield accountItemShield = db.AccountItemShields.Find(id);
            db.AccountItemShields.Remove(accountItemShield);
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
