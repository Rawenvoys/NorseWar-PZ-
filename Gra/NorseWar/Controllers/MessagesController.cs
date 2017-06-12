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
            if (Session["Admin"] == "admin")
            {
                return View(db.Messages.ToList());
            }
            else
                return RedirectToAction("Login", "Panel");            
        }

        public ActionResult YourMessages()
        {
            var main = new MainMessageModel();
            var user = (Account)Session["User"];
            if (user != null)
            {
                main.Messages = Methods.ShowMessages(user).OrderByDescending(x => x.Date).ToList();
                main.OneMessage = null;
                return View(main);
            }
            else
                return RedirectToAction("Login", "Panel");       
        }

        [HttpPost]
        public ActionResult YourMessages(MainMessageModel main, int id, string tit)
        {
            var model = new MainMessageModel();
            var user = (Account)Session["User"];
            model.Messages = Methods.ShowMessages(user).OrderByDescending(x => x.Date).ToList();
            model.OneMessage = main.OneMessage;

            var mess = new Message();
            mess.Date = DateTime.Now;
            mess.Status = false;
            mess.SenderId = user.AccountID;
            mess.RecipentId = id;
            mess.Text = main.OneMessage.Text;
            mess.Title = tit;

            db.Messages.Add(mess);
            db.SaveChanges();

            return View(model);
        }


        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == "admin")
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
            else
                return RedirectToAction("Login", "Panel");    
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            if (Session["User"]!=null)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Panel");            
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
            if (Session["Admin"] == "admin")
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
            else
                return RedirectToAction("Login", "Panel"); 
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
            if (Session["Admin"] == "admin")
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
            else
                return RedirectToAction("Login", "Panel");    
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
            var result = Methods.ShowLoginFromId(id);
            return Json(result);
        }

        public void ChangeMessageStatus(int id)
        {
            var mess = db.Messages.Find(id);
            mess.Status = true;
            db.SaveChanges();
        }

        public void DeleteMessage(int id)
        {
            var mess = db.Messages.Find(id);
            db.Messages.Remove(mess);
            db.SaveChanges();
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
