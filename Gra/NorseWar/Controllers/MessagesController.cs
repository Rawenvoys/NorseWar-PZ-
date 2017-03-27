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
    public class MessagesController : Controller
    {
        private GameContext db = new GameContext();

        public ActionResult Index()
        {
            return View(db.Messages.ToList());
        }


        //public ActionResult YourMessages()
        //{
        //    var user = (Account)Session["User"];
        //    MainMessageModel mmm = new MainMessageModel();
        //    mmm.Messages = Methods.ShowMessages(user);
        //    return View(mmm);
        //}

        //[HttpPost]
        //public ActionResult YourMessages(Message message)
        //{
        //    MainMessageModel mmm = new MainMessageModel();
        //    mmm.OneMessage = message;
        //    var user = (Account)Session["User"];
        //    mmm.Messages = Methods.ShowMessages(user);
        //    return View(mmm);
        //}

        public ActionResult YourMessages()
        {
            var user = (Account)Session["User"];
            return View(Methods.ShowMessages(user));
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageId,Title,Text")] Message message, int id)
        {
            if (ModelState.IsValid)
            {
                var user = (Account)Session["User"];
                message.SenderId = user.AccountID;
                message.RecipentId = id;

                message.Date = DateTime.Now;
                message.Status = false;

                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Details", "User", new { id = id});
            }

            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageId,SenderId,RecipentId,Title,Text,Date,Status")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetMessage(int id)
        {
            var mes = Methods.ShowMessageById(id);
            return Json(mes);
        }


        public JsonResult SenderLogin(int id)
        {
            var result = Methods.ShowSenderLogin(id);
            return Json(result);
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
