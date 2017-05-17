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
    public class ItemShieldsController : Controller
    {
        //private GameContext db = new GameContext();

        //// GET: ItemShields
        //public ActionResult Index()
        //{
        //    return View(db.ItemShields.ToList());
        //}

        //// GET: ItemShields/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ItemShield itemShield = db.ItemShields.Find(id);
        //    if (itemShield == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(itemShield);
        //}

        //// GET: ItemShields/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ItemShields/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ItemShieldID,Name,Description,StrBonus,AgiBonus,DexBonus,VitBonus,IntBonus,LukBonus,Url")] ItemShield itemShield)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        itemShield.Type = "Shield";
        //        db.ItemShields.Add(itemShield);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(itemShield);
        //}

        //// GET: ItemShields/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ItemShield itemShield = db.ItemShields.Find(id);
        //    if (itemShield == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(itemShield);
        //}

        //// POST: ItemShields/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ItemShieldID,Name,Description,StrBonus,AgiBonus,DexBonus,VitBonus,IntBonus,LukBonus,Url")] ItemShield itemShield)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        itemShield.Type = "Shield";
        //        db.Entry(itemShield).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(itemShield);
        //}

        //// GET: ItemShields/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ItemShield itemShield = db.ItemShields.Find(id);
        //    if (itemShield == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(itemShield);
        //}

        //// POST: ItemShields/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ItemShield itemShield = db.ItemShields.Find(id);
        //    db.ItemShields.Remove(itemShield);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
