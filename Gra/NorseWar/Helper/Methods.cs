using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorseWar.Models;
using NorseWar.Models.DAL;

namespace NorseWar.Helper
{
    public class Methods
    {
        public static int? userId { get; set; }
        public static string userMail { get; set; }
        public static Account account { get; set; }
        public static void SaveUserSession(int? id, string mail, Account acc)
        {
            userId = id;
            userMail = mail;
            account = acc;
        }




        public static string RegisterSuccess { get; set; }  //powodzenie rejestracji
        public static string LoginFailed { get; set; }   //nie ma takiego konta
        public static string AccountActive { get; set; }   //konto zajete


        public static List<Quest> ShowQuestions()
        {
            GameContext db = new GameContext();

            try
            {
                var result = db.AccountQuestes.Single(x => x.AccountId == userId);
                var q1 = db.Quests.Find(result.Quest1);
                var q2 = db.Quests.Find(result.Quest2);
                var q3 = db.Quests.Find(result.Quest3);

                List<Quest> list = new List<Quest> { q1, q2, q3 };
                return list;
            }
            catch
            {
                Random rand = new Random();
                var questsList = db.Quests.ToList();
                var questOrder = questsList.OrderBy(x => rand.Next(questsList.Count)).Take(3).ToList();

                AccountQuests aq = new AccountQuests();
                aq.AccountId = (int)userId;
                aq.Quest1 = questOrder[0].Id;
                aq.Quest2 = questOrder[1].Id;
                aq.Quest3 = questOrder[2].Id;

                db.AccountQuestes.Add(aq);
                db.SaveChanges();
                return questOrder;
            }   
        }
    }
}