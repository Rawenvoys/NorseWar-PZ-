using NorseWar.Helper;
using NorseWar.Models;
using NorseWar.Models.BattleModel;
using NorseWar.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorseWar.Controllers
{
    public class BattleAreaController : Controller
    {
        GameContext db = new GameContext();

        public ActionResult Index()
        {
            var user = (Account)Session["User"];
            Tuple<Account, Account> accounts = Methods.GetUsers(user);
            return View(accounts);
        }

        public ActionResult Fight(int id)
        {
            //string cashResult = "";
            //bool battleResult = false;
            var user = (Account)Session["User"];
            Account enemy = db.Accounts.Find(id);
            StatsInfo userInfo = Methods.CalculateBattleStats(user);
            StatsInfo enemyInfo = Methods.CalculateBattleStats(enemy);
            var enemies = new Tuple<Account, StatsInfo, Account, StatsInfo>(enemy, enemyInfo, user, userInfo);

            Battle battle = Methods.BattleTable(userInfo, enemyInfo, enemy);
           


            //Account winner = Methods.PickWinner(enemies);


            //if (account1.Gold <= 0)
            //value = 0;

            //if (winner.Equals(enemy))
            //{
            //    cashResult = ((user.Gold / 10) * (-1)).ToString();
            //    battleResult = false;
            //    Methods.SendGold(user.AccountID, enemy.AccountID, user.Gold / 10);
            //}
            //else if (winner.Equals(user))
            //{
            //    cashResult = (enemy.Gold / 10).ToString();
            //    battleResult = true;
            //    Methods.SendGold(enemy.AccountID, user.AccountID, user.Gold / 10);
            //}
            //Tuple<Account, string, bool> result = new Tuple<Account, string, bool>(winner, cashResult, battleResult);
            return View(battle);

        }
    }
}