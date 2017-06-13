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
    public class QuestsController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Quests
        public ActionResult Index()
        {
            if (Session["Admin"] == "admin")
            {
                return View(db.Quests.ToList());
            }
            else
                return RedirectToAction("Login", "Panel"); 
        }

        // GET: Quests/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Quest quest = db.Quests.Find(id);
                if (quest == null)
                {
                    return HttpNotFound();
                }
                return View(quest);
            }
            else
                return RedirectToAction("Login", "Panel");  
        }

        // GET: Quests/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == "admin")
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Panel"); 
        }

        // POST: Quests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,TimeSecond,ExpValue")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                db.Quests.Add(quest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quest);
        }

        // GET: Quests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Quest quest = db.Quests.Find(id);
                if (quest == null)
                {
                    return HttpNotFound();
                }
                return View(quest);
            }
            else
                return RedirectToAction("Login", "Panel");   
        }

        // POST: Quests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,TimeSecond,ExpValue")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quest);
        }

        // GET: Quests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Quest quest = db.Quests.Find(id);
                if (quest == null)
                {
                    return HttpNotFound();
                }
                return View(quest);
            }
            else
                return RedirectToAction("Login", "Panel");   
        }

        // POST: Quests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quest quest = db.Quests.Find(id);
            db.Quests.Remove(quest);
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
