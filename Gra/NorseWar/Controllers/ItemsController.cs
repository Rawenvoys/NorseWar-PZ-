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
    public class ItemsController : Controller
    {
        private GameContext db = new GameContext();

        public ActionResult Index()
        {
            if (Session["Admin"] == "admin")
            {
                return View(db.Items.ToList());
            }
            else
                return RedirectToAction("Login", "Panel");    
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Item item = db.Items.Find(id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                return View(item);
            }
            else
                return RedirectToAction("Login", "Panel");      
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == "admin")
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Panel");    
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StrBonus,AgiBonus,DexBonus,VitBonus,IntBonus,LukBonus,Url,Type")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Item item = db.Items.Find(id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                return View(item);
            }
            else
                return RedirectToAction("Login", "Panel");     
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StrBonus,AgiBonus,DexBonus,VitBonus,IntBonus,LukBonus,Url,Type")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Item item = db.Items.Find(id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                return View(item);
            }
            else
                return RedirectToAction("Login", "Panel");        
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
