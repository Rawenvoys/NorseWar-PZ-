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
    public class StatsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Stats
        public ActionResult Index()
        {
            if (Session["Admin"] == "admin")
            {
                var statses = db.Statses.Include(s => s.Account);
                return View(statses.ToList());
            }
            else
                return RedirectToAction("Login", "Panel");    
        }

        // GET: Stats/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Stats stats = db.Statses.Find(id);
                if (stats == null)
                {
                    return HttpNotFound();
                }
                return View(stats);
            }
            else
                return RedirectToAction("Login", "Panel");   
        }

        // GET: Stats/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == "admin")
            {
                ViewBag.StatsID = new SelectList(db.Accounts, "AccountID", "Login");
                return View();
            }
            else
                return RedirectToAction("Login", "Panel");  
        }

        // POST: Stats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatsID,Str,Agi,Dex,Vit,Int,Luk")] Stats stats)
        {
            if (ModelState.IsValid)
            {
                db.Statses.Add(stats);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatsID = new SelectList(db.Accounts, "AccountID", "Login", stats.StatsID);
            return View(stats);
        }

        // GET: Stats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Stats stats = db.Statses.Find(id);
                if (stats == null)
                {
                    return HttpNotFound();
                }
                ViewBag.StatsID = new SelectList(db.Accounts, "AccountID", "Login", stats.StatsID);
                return View(stats);
            }
            else
                return RedirectToAction("Login", "Panel");            
        }

        // POST: Stats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatsID,Str,Agi,Dex,Vit,Int,Luk")] Stats stats)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stats).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Accounts");
            }
            ViewBag.StatsID = new SelectList(db.Accounts, "AccountID", "Login", stats.StatsID);
            return View(stats);
        }

        // GET: Stats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Stats stats = db.Statses.Find(id);
                if (stats == null)
                {
                    return HttpNotFound();
                }
                return View(stats);
            }
            else
                return RedirectToAction("Login", "Panel");   
        }

        // POST: Stats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stats stats = db.Statses.Find(id);
            db.Statses.Remove(stats);
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
