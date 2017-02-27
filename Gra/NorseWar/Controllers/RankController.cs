using NorseWar.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorseWar.Models;
using NorseWar.Helper;

namespace NorseWar.Controllers
{
    public class RankController : Controller
    {
        private GameContext db = new GameContext();
        public ActionResult Index()
        {
            var model = new MainModel();
            var user = (Account)Session["User"];
            model.Account = Methods.ShowRanking(user);
            return View(model);


            
         //   return View(Methods.ShowRanking(user));
        }

        [HttpPost]
        public ActionResult Index(MainModel mainModel)
        {
            Account acc = SearchUser.Search(mainModel.SearchUser);

            var model = new MainModel();
            
            model.Account = Methods.ShowRanking(acc);
            return View(model);
        }
    }
}