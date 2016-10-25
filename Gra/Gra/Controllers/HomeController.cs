using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Gra.Helper;
using Gra.Models;

namespace Gra.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(Account account)
        {
            //using (var sC = Methods.GetSqlConnection(Consts.DATABASE))
            //{
            //    if (sC == null)
            //        return View();
            //    return View();

            //}
            using (AccountDbContext aDC = new AccountDbContext())
            {
                var user = aDC.accountContext.Single(u => u.Login == account.Login && u.Password == account.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", Consts.LOGIN_ERROR);
                    return View();
                }
                Session[Consts.SESSION_ID] = user.Id.ToString();
                Session[Consts.SESSION_LOGIN] = user.Login.ToString();
                return RedirectToAction("Main");
            }

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (!ModelState.IsValid)
                ModelState.Clear();
            using (AccountDbContext aDC = new AccountDbContext())
            {
                aDC.accountContext.Add(account);
                aDC.SaveChanges();
            }
            return View();
        }

        public ActionResult Main()
        {
            if (Session[Consts.SESSION_ID] == null)
                return RedirectToAction("Index");
            return View();
        }
    }
}