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
            if (user != null)
            {
                Tuple<Account, Account> accounts = Methods.GetUsers(user);
                return View(accounts);
            }
            else
                return RedirectToAction("Login", "Panel");   
        }

        public ActionResult Fight(int id)
        {
            //string cashResult = "";
            //bool battleResult = false;
            var user = (Account)Session["User"];
            if (user != null)
            {
                Account enemy = db.Accounts.Find(id);
                StatsInfo userInfo = Methods.CalculateBattleStats(user);
                StatsInfo enemyInfo = Methods.CalculateBattleStats(enemy);
                var enemies = new Tuple<Account, StatsInfo, Account, StatsInfo>(enemy, enemyInfo, user, userInfo);
                Battle battle = Methods.BattleTable(userInfo, enemyInfo, enemy);

                var enemyGold = enemy.Gold;
                var userGold = user.Gold;
                var hits = battle.Hits;

                var user2 = db.Accounts.Find(user.AccountID);

                int player1dmg = 0;
                int player2dmg = 0;
                double player1dmgvar = 0;
                double player2dmgvar = 0;
                foreach (var item in hits)
                {
                    if (item.Player1Dmg > 0) player1dmg++;
                    if (item.Player2Dmg > 0) player2dmg++;
                }

                if(player1dmg > player2dmg)//1 wygral
                {
                    if(enemyGold > 0)
                    {
                        double getGold = enemyGold * 0.05;
                        enemy.Gold -= (int)getGold;
                        user2.Gold += (int)getGold;
                    }
                }
                else if(player2dmg > player1dmg)//2 wygral
                {
                    if(userGold > 0)
                    {
                        double getGold = userGold * 0.05;
                        user2.Gold -= (int)getGold;
                        enemy.Gold += (int)getGold;
                    }
                }
                else
                {
                    if(player1dmg == player2dmg && player2dmg == 1)
                    {
                        foreach (var item in hits)
                        {
                            player1dmgvar = item.Player1Dmg;
                            player2dmgvar = item.Player2Dmg;
                        }

                        if(player1dmgvar > battle.Player2Hp)//1 wygrywa
                        {
                            if (enemyGold > 0)
                            {
                                double getGold = enemyGold * 0.05;
                                enemy.Gold -= (int)getGold;
                                user2.Gold += (int)getGold;
                            }
                        }
                        else if(player2dmgvar > battle.Player1Hp)//2 wygrywa
                        {
                            if (userGold > 0)
                            {
                                double getGold = userGold * 0.05;
                                user2.Gold -= (int)getGold;
                                enemy.Gold += (int)getGold;
                            }
                        }
                    }
                }

                db.SaveChanges();
                return View(battle);
            }
            else
                return RedirectToAction("Login", "Panel");



            //if (account1.Gold <= 0)
            //    value = 0;

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


        }
    }
}