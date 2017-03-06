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
    public class AccountMessagesController : Controller
    {
        private GameContext db = new GameContext();

        // GET: AccountMessages
        public ActionResult Index()
        {
            var accountMessages = db.AccountMessages.Include(a => a.Account).Include(a => a.Message);
            return View(accountMessages.ToList());
        }

        // GET: AccountMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountMessage accountMessage = db.AccountMessages.Find(id);
            if (accountMessage == null)
            {
                return HttpNotFound();
            }
            return View(accountMessage);
        }

        // GET: AccountMessages/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login");
            ViewBag.MessageID = new SelectList(db.Messages, "MessageID", "Title");
            return View();
        }

        // POST: AccountMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountMessageID,AccountID,MessageID")] AccountMessage accountMessage)
        {
            if (ModelState.IsValid)
            {
                db.AccountMessages.Add(accountMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountMessage.AccountID);
            ViewBag.MessageID = new SelectList(db.Messages, "MessageID", "Title", accountMessage.MessageID);
            return View(accountMessage);
        }

        // GET: AccountMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountMessage accountMessage = db.AccountMessages.Find(id);
            if (accountMessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountMessage.AccountID);
            ViewBag.MessageID = new SelectList(db.Messages, "MessageID", "Title", accountMessage.MessageID);
            return View(accountMessage);
        }

        // POST: AccountMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountMessageID,AccountID,MessageID")] AccountMessage accountMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountMessage.AccountID);
            ViewBag.MessageID = new SelectList(db.Messages, "MessageID", "Title", accountMessage.MessageID);
            return View(accountMessage);
        }

        // GET: AccountMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountMessage accountMessage = db.AccountMessages.Find(id);
            if (accountMessage == null)
            {
                return HttpNotFound();
            }
            return View(accountMessage);
        }

        // POST: AccountMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountMessage accountMessage = db.AccountMessages.Find(id);
            db.AccountMessages.Remove(accountMessage);
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
