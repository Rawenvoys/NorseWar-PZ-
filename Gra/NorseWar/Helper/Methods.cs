﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorseWar.Models;
using NorseWar.Models.DAL;

namespace NorseWar.Helper
{
    public class Methods
    {
        public static string RegisterSuccess { get; set; }  //powodzenie rejestracji
        public static string LoginFailed { get; set; }   //nie ma takiego konta
        public static string AccountActive { get; set; }   //konto zajete



        public static List<Quest> ShowQuestions(int uId)
        {
            GameContext db = new GameContext();
            try
            {
                var result = db.AccountQuestes.Single(x => x.AccountId == uId);
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
                aq.AccountId = uId;
                aq.Quest1 = questOrder[0].Id;
                aq.Quest2 = questOrder[1].Id;
                aq.Quest3 = questOrder[2].Id;

                db.AccountQuestes.Add(aq);
                db.SaveChanges();
                return questOrder;
            }   
        }


        public static List<Message> ShowMessages(Account user)
        {
            GameContext db = new GameContext();
            var anyUser = db.Messages.Any(x => x.RecipentId == user.AccountID);
            if (anyUser)
            {
                var result = db.Messages.Where(x => x.RecipentId == user.AccountID).ToList();
                return result;
            }
            else
            {
                List<Message> list = new List<Message>();
                return list;
            }    
        }



        public static List<Account> ShowRanking(Account user)
        {
            GameContext db = new GameContext();
            try
            {
                List<Account> acc = db.Accounts.OrderByDescending(x => x.Experience).ToList();
                int getUser = acc.FindIndex(x => x.AccountID == user.AccountID);
                List<Account> resultList = new List<Account>();

                //3,4,5,6..
                if (getUser > 2)
                {
                    for (int i = getUser - 1; i >= getUser - 3; i--)
                    {
                        resultList.Add(acc[i]);
                    }
                    resultList.Add(user);

                    //3 4 5
                    if (getUser + 3 <= acc.Count - 1)
                    {
                        for (int i = getUser + 1; i <= getUser + 3; i++)
                        {
                            resultList.Add(acc[i]);
                        }
                    }

                    else
                    {
                        for (int i = getUser + 1; i < acc.Count; i++)
                        {
                            resultList.Add(acc[i]);
                        }
                    }
                }

                //0
                else if (getUser == 0)
                {
                    resultList.Add(user);
                    if (getUser + 3 <= acc.Count)
                    {
                        for (int i = getUser + 1; i <= getUser + 3; i++)
                        {
                            resultList.Add(acc[i]);
                        }
                    }
                    else
                    {
                        for (int i = getUser + 1; i < acc.Count; i++)
                        {
                            resultList.Add(acc[i]);
                        }
                    }
                }

                //1,2,
                else
                {
                    for (int i = getUser - 1; i >= 0; i--)
                    {
                        resultList.Add(acc[i]);
                    }
                    resultList.Add(user);

                    if (getUser + 3 <= acc.Count)
                    {
                        for (int i = getUser + 1; i <= getUser + 3; i++)
                        {
                            resultList.Add(acc[i]);
                        }
                    }

                    else
                    {
                        for (int i = getUser + 1; i < acc.Count; i++)
                        {
                            resultList.Add(acc[i]);
                        }
                    }
                }

               // var lista = resultList.OrderByDescending(x => x.Experience).ToList();
                return resultList.OrderByDescending(x => x.Experience).ToList();
            }
            catch
            {
                List<Account> acc = new List<Account>();
                return acc;
            }
        }
    }
}