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
                    if (checkData.BanTime > DateTime.Now && checkData.BanTime != null)
                    {
                        Methods.LoginFailed = "BanTime";
                        return View();
                    }
                    else
                    {
                        checkData.BanTime = null;
                        db.SaveChanges();
                    }
                    Session["userId"] = checkData.AccountID;
                    Session.Timeout = 3;



                  //  Methods.SaveUserSession(checkData.AccountID, checkData.Mail, checkData);
                    Methods.LoginFailed = null;
                    return RedirectToAction("Tavern", "User");

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
                        account.Experience = 0;
                        account.Gold = 100;
                        account.BanTime = null;
                        ItemShield iS1 = db.ItemShields.Find(1);
                        ItemWeapon iW1 = db.ItemWeapons.Find(1); //KNIFE
                        ItemWeapon iW2 = db.ItemWeapons.Find(2); //BOW
                        ItemWeapon iW3 = db.ItemWeapons.Find(3); //ROD
                        Stats stats = new Stats(account, account.CharacterClass);
                        account.Stats = stats;
                        db.Accounts.Add(account);
                        
                        db.SaveChanges();
                        Methods.RegisterSuccess = "Success";
                        Methods.AccountActive = null;
                        //zapisuje do methods
                        Account acc = db.Accounts.Single(a => a.Login == account.Login);
                        if (account.CharacterClass == NorseWar.Models.Characters.Mage)
                        {
                            AccountItemWeapon aIW = new AccountItemWeapon(acc,iW3); 
                            db.AccountItemWeapons.Add(aIW);
                        }
                        if (account.CharacterClass == NorseWar.Models.Characters.Archer)
                        {
                            AccountItemWeapon aIW = new AccountItemWeapon(acc, iW2);
                            db.AccountItemWeapons.Add(aIW);
                        }
                        if (account.CharacterClass == NorseWar.Models.Characters.Warrior)
                        {
                            AccountItemWeapon aIW = new AccountItemWeapon(acc, iW1);
                            AccountItemShield aIS = new AccountItemShield(acc,iS1);
                            db.AccountItemWeapons.Add(aIW);
                            db.AccountItemShields.Add(aIS);
                        }
                        db.SaveChanges();
                        return RedirectToAction("Login", "Panel");
                    }
                }
            }
            return View(account);
        }


        public ActionResult LogOut()
        {
            Session["userId"] = null;
         //   Methods.SaveUserSession(null, null,null);
            return RedirectToAction("Login", "Panel");
        }


    }
}
