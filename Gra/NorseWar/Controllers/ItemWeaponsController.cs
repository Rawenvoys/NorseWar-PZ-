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
    public class ItemWeaponsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: ItemWeapons
        public ActionResult Index()
        {
            return View(db.ItemWeapons.ToList());
        }

        // GET: ItemWeapons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemWeapon itemWeapon = db.ItemWeapons.Find(id);
            if (itemWeapon == null)
            {
                return HttpNotFound();
            }
            return View(itemWeapon);
        }

        // GET: ItemWeapons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemWeapons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemWeaponID,Name,Description,Character,StrBonus,AgiBonus,DexBonus,VitBonus,IntBonus,LukBonus")] ItemWeapon itemWeapon)
        {
            if (ModelState.IsValid)
            {
                db.ItemWeapons.Add(itemWeapon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itemWeapon);
        }

        // GET: ItemWeapons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemWeapon itemWeapon = db.ItemWeapons.Find(id);
            if (itemWeapon == null)
            {
                return HttpNotFound();
            }
            return View(itemWeapon);
        }

        // POST: ItemWeapons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemWeaponID,Name,Description,Character,StrBonus,AgiBonus,DexBonus,VitBonus,IntBonus,LukBonus")] ItemWeapon itemWeapon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemWeapon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemWeapon);
        }

        // GET: ItemWeapons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemWeapon itemWeapon = db.ItemWeapons.Find(id);
            if (itemWeapon == null)
            {
                return HttpNotFound();
            }
            return View(itemWeapon);
        }

        // POST: ItemWeapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemWeapon itemWeapon = db.ItemWeapons.Find(id);
            db.ItemWeapons.Remove(itemWeapon);
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
