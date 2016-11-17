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
    public class AccountItemWeaponsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: AccountItemWeapons
        public ActionResult Index()
        {
            var accountItemWeapons = db.AccountItemWeapons.Include(a => a.Account).Include(a => a.ItemWeapon);
            return View(accountItemWeapons.ToList());
        }

        // GET: AccountItemWeapons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountItemWeapon accountItemWeapon = db.AccountItemWeapons.Find(id);
            if (accountItemWeapon == null)
            {
                return HttpNotFound();
            }
            return View(accountItemWeapon);
        }

        // GET: AccountItemWeapons/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login");
            ViewBag.ItemWeaponID = new SelectList(db.ItemWeapons, "ItemWeaponID", "Name");
            return View();
        }

        // POST: AccountItemWeapons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountItemWeaponID,ItemWeaponID,AccountID,Equiped")] AccountItemWeapon accountItemWeapon)
        {
            if (ModelState.IsValid)
            {
                db.AccountItemWeapons.Add(accountItemWeapon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountItemWeapon.AccountID);
            ViewBag.ItemWeaponID = new SelectList(db.ItemWeapons, "ItemWeaponID", "Name", accountItemWeapon.ItemWeaponID);
            return View(accountItemWeapon);
        }

        // GET: AccountItemWeapons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountItemWeapon accountItemWeapon = db.AccountItemWeapons.Find(id);
            if (accountItemWeapon == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountItemWeapon.AccountID);
            ViewBag.ItemWeaponID = new SelectList(db.ItemWeapons, "ItemWeaponID", "Name", accountItemWeapon.ItemWeaponID);
            return View(accountItemWeapon);
        }

        // POST: AccountItemWeapons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountItemWeaponID,ItemWeaponID,AccountID,Equiped")] AccountItemWeapon accountItemWeapon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountItemWeapon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Login", accountItemWeapon.AccountID);
            ViewBag.ItemWeaponID = new SelectList(db.ItemWeapons, "ItemWeaponID", "Name", accountItemWeapon.ItemWeaponID);
            return View(accountItemWeapon);
        }

        // GET: AccountItemWeapons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountItemWeapon accountItemWeapon = db.AccountItemWeapons.Find(id);
            if (accountItemWeapon == null)
            {
                return HttpNotFound();
            }
            return View(accountItemWeapon);
        }

        // POST: AccountItemWeapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountItemWeapon accountItemWeapon = db.AccountItemWeapons.Find(id);
            db.AccountItemWeapons.Remove(accountItemWeapon);
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
