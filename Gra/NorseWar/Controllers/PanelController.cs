using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorseWar.Helper;
using NorseWar.Models;
using NorseWar.Models.DAL;
using System.Data.SqlClient;

namespace NorseWar.Controllers
{
    public class PanelController : Controller
    {
        private GameContext db = new GameContext();

        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            Methods.RegisterSuccess = null;
            using (db)
            {
                try
                {
                    var checkData = db.Accounts.Single(u => u.Login == account.Login && u.Password == account.Password);
                    Methods.SaveUserSession(checkData.AccountID, checkData.Mail);
                    Methods.LoginFailed = null;
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    Methods.LoginFailed = "Error";
                    return View();
                }
            }
        }


        public ActionResult Register()
        {
            //SqlCommand command = new SqlCommand("DROP DATABASE GameContext2");
         //   SqlCommand command = new SqlCommand("DROP DATABASE ItemSet");
        //    command.Connection.Open();
       //     command.ExecuteNonQuery();
            return View();
        }


        [HttpPost]
        public ActionResult Register([Bind(Include = "AccountID,Login,Password,Mail,CharacterClass")] Account account)
        {

            if (ModelState.IsValid)
            {
                using (db)
                {
                    try
                    {
                        var checkData = db.Accounts.Single(u => u.Login == account.Login);
                        Methods.AccountActive = "Already";
                        return View(account);
                    }
                    catch
                    {
                        db.Accounts.Add(account);
                        db.SaveChanges();
                        Methods.RegisterSuccess = "Success";
                        Methods.AccountActive = null;
                        //zapisuje do methods
                        return RedirectToAction("Login", "Panel");
                    }
                }
            }
            return View(account);
        }


    }
}



/*
 
    jesli sie wyloguje to dac nulla i chuj

 public ActionResult LogOut()
        {
            //   Session["UserId"] = null;
            Methods.SaveUserSession(null, null);
            return RedirectToAction("Index");
        }
*/
