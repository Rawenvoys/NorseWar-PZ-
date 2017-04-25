﻿using NorseWar.Helper;
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

        // GET: BattleArea
        public ActionResult Index()
        {
            var user = (Account)Session["User"];
            Tuple<Account, Account> accounts = Methods.GetUsers(user);
            return View(accounts);
        }

        public ActionResult Fight(int id)
        {
            var user = (Account)Session["User"];
            Account enemy = db.Accounts.Where(x => x.AccountID == id).Single();
            StatsInfo userInfo = Methods.CalculateBattleStats(user);
            StatsInfo enemyInfo = Methods.CalculateBattleStats(enemy);
            var enemies = new Tuple<Account, StatsInfo, Account, StatsInfo>(enemy, enemyInfo, user, userInfo);
            Account winner = Methods.PickWinner(enemies);
            string cashResult = "";
            bool battleResult = false;
            if (winner.Equals(enemy))
            {
                cashResult = ((user.Gold / 10) * (-1)).ToString();
                battleResult = false;
            }
            else if (winner.Equals(user))
            {
                cashResult = (enemy.Gold / 10).ToString();
                battleResult = true;
            }
            Tuple<Account, string, bool> result = new Tuple<Account, string, bool>(winner, cashResult, battleResult);
            return View(result);

        }
    }
}