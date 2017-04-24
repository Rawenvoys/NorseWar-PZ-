using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorseWar.Models;
using NorseWar.Models.DAL;
using System.Web.Mvc;

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
                var result = db.AccountQuestes.Single(x => x.AccountId == uId); //To trzeba zmienić   && x.Id>=8
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


        public static int ShowPosition(int id)
        {
            GameContext db = new GameContext();
            List<Account> acc = db.Accounts.OrderByDescending(x => x.Experience).ToList();
            int getUser = acc.FindIndex(x => x.AccountID == id);
            return getUser+1;
        }


        public static Account ShowUserFromPosition(int position)
        {
            GameContext db = new GameContext();
            List<Account> acc = db.Accounts.OrderByDescending(x => x.Experience).ToList();

            //JESLI POZYCJA WIEKSZA NIZ ILOSC ZIOMKOW TO DAC INFO ZE NIE MA TYLU GRACZY XD

            if(position > acc.Count)
            {
                Account account = new Account();
                return account;
            }
            else
                return acc.ElementAt(position - 1);
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


        public static string ShowLoginFromId(int id)
        {
            GameContext db = new GameContext();
            var user = db.Accounts.Find(id);
            return user.Login;
        }

        public static string ChangeData(DateTime data)
        {
            var year = data.Year;
            var month = data.Month;
            var day = data.Day;

            var hour = data.Hour;
            var minute = data.Minute;

            string mm = null;
            mm = (minute < 10) ? "0"+minute : minute.ToString();

            return day + "." + month + "." + year + " " + hour + ":" + mm;
        }


        public static Message ShowMessageById(int id)
        {
            GameContext db = new GameContext();
            var mess = db.Messages.Find(id);
            return mess;
        }


        public static int ShowUserLevel(Account user)
        {
            int level = 0;
            for (int i = 1; i < LevelTable.Level.Count(); i++)
            {
                if (LevelTable.Level[i] > user.Experience)
                {
                    level = i - 1;
                    break;
                }
            }
            return level;
        }


        public static Account GetUserFromBase(Account user)
        {
            GameContext db = new GameContext();
            return db.Accounts.Find(user.AccountID);
        }


        public static int SetMoney(int time, int lvl)
        {
            return (lvl * time * 4) - (lvl - (time / 10));
        }


        public static void StartGuard(int id, Account user)
        {
            GameContext db = new GameContext();
            Guard guar = new Guard();
            guar.AccountID = user.AccountID;
            guar.Money = SetMoney(id, ShowUserLevel(user));
            guar.GuardEndTime = DateTime.Now.AddHours(id);
            guar.GuardStartTime = DateTime.Now;
            db.Quards.Add(guar);
            db.SaveChanges();
        }


        public static void CancelGuard(Account user)
        {
            GameContext db = new GameContext();
            var guard = db.Quards.Single(x => x.AccountID == user.AccountID);
            db.Quards.Remove(guard);
            db.SaveChanges();
        }


        public static int[] ShowGuardEndTime(Account user)
        {
            GameContext db = new GameContext();
            var guard = db.Quards.Single(x => x.AccountID == user.AccountID);
            var endTime = guard.GuardEndTime.TimeOfDay.TotalSeconds;
            var startTime = guard.GuardStartTime.TimeOfDay.TotalSeconds;
            var now = DateTime.Now.TimeOfDay.TotalSeconds;
            var left = endTime - now;
            int[] arr = {0, 0, 0};
            arr[0] = (int)startTime;
            arr[1] = (int)left;
            arr[2] = (int)endTime;
            if (startTime > endTime)
            {
                var time = new TimeSpan(23, 59, 59);
                var newEndTime = time.TotalSeconds;
                var newStartTime = startTime - endTime;
                var newNow = newStartTime;
                if(now < endTime)
                {
                    newNow = endTime - now;
                }else
                { 
                    newNow = (time.TotalSeconds - now) + (endTime - new TimeSpan(0,0,0).TotalSeconds);
                }
                
                arr[0] = (int)newStartTime;
                arr[1] = (int)newNow;
                arr[2] = (int)newEndTime;
            }            
            return arr;
        }


        public static int AddPoint(int id, Account user)
        {
            GameContext db = new GameContext();
            var baseUser = db.Accounts.Find(user.AccountID);
            int value = 0;
            int money = 0;

            switch (id)
            {
                case 0:
                    money = baseUser.Stats.Str + 1;
                    if (baseUser.Gold >= money)
                    {
                        baseUser.Gold -= money;
                        baseUser.Stats.Str++;
                        db.SaveChanges();
                        value = baseUser.Stats.Str;
                        break;
                    }
                    else
                    {
                        value = baseUser.Stats.Str;
                        break;
                    }
                    
                case 1:
                    money = baseUser.Stats.Agi + 1;
                    if (baseUser.Gold >= money)
                    {
                        baseUser.Gold -= money;
                        baseUser.Stats.Agi++;
                        db.SaveChanges();
                        value = baseUser.Stats.Agi;
                        break;
                    }
                    else
                    {
                        value = baseUser.Stats.Agi;
                        break;
                    }
                        
                case 2:
                    money = baseUser.Stats.Vit + 1;
                    if (baseUser.Gold >= money)
                    {
                        baseUser.Gold -= money;
                        baseUser.Stats.Vit++;
                        db.SaveChanges();
                        value = baseUser.Stats.Vit;
                        break;
                    }
                    else
                    {
                        value = baseUser.Stats.Vit;
                        break;
                    }

                case 3:
                    money = baseUser.Stats.Dex + 1;
                    if (baseUser.Gold >= money)
                    {
                        baseUser.Gold -= money;
                        baseUser.Stats.Dex++;
                        db.SaveChanges();
                        value = baseUser.Stats.Dex;
                        break;
                    }
                    else
                    {
                        value = baseUser.Stats.Dex;
                        break;
                    }

                case 4:
                    money = baseUser.Stats.Int + 1;
                    if (baseUser.Gold >= money)
                    {
                        baseUser.Gold -= money;
                        baseUser.Stats.Int++;
                        db.SaveChanges();
                        value = baseUser.Stats.Int;
                        break;
                    }
                    else
                    {
                        value = baseUser.Stats.Int;
                        break;
                    }

                case 5:
                    money = baseUser.Stats.Luk + 1;
                    if (baseUser.Gold >= money)
                    {
                        baseUser.Gold -= money;
                        baseUser.Stats.Luk++;
                        db.SaveChanges();
                        value = baseUser.Stats.Luk;
                        break;
                    }
                    else
                    {
                        value = baseUser.Stats.Luk;
                        break;
                    }
            }
            return value;
        }


        public static bool CheckGuard(Account user)
        {
            GameContext db = new GameContext();
            var result = db.Quards.Any(x => x.AccountID == user.AccountID);

            if (result)
            {
                var guardCheck = db.Quards.Where(x => x.AccountID == user.AccountID).ToList();
                if (guardCheck.Count > 1)
                {
                    for (int i = 1; i < guardCheck.Count; i++)
                    {
                        db.Quards.Remove(guardCheck[i]);
                    }
                    db.SaveChanges();

                    if (DateTime.Now > guardCheck[0].GuardEndTime)
                    {
                        var acc = db.Accounts.Find(user.AccountID);
                        acc.Gold += guardCheck[0].Money;
                        db.Quards.Remove(guardCheck[0]);  //USUN TA LINIE XD
                        db.SaveChanges();
                        return false;
                    }

                    else return true;
                }
                else
                {
                    if (DateTime.Now > guardCheck[0].GuardEndTime)
                    {
                        var acc = db.Accounts.Find(user.AccountID);
                        acc.Gold += guardCheck[0].Money;
                        db.Quards.Remove(guardCheck[0]);  //USUN TA LINIE XD
                        db.SaveChanges();
                        return false;
                    }
                    else return true;
                }
            }
            else return false;
        }


        public static List<Stats> Arena3Players(Account user)
        {
            GameContext db = new GameContext();
            var userStats = db.Statses.Find(user.AccountID);
            List<Account> acc = db.Accounts.OrderByDescending(x => x.Experience).ToList();
            List<Stats> statList = new List<Stats>();

            int pos = ShowPosition(user.AccountID);
            int position = pos - 1;

            if (position == 0)
            {
                for (int i = position + 1; i < position + 4; i++)
                {
                    var statt = db.Statses.Find(acc[i].AccountID);
                    statList.Add(statt);
                }
            }

            else if (position == acc.Count)
            {
                for (int i = position - 1; i < position - 4; i++)
                {
                    var statt = db.Statses.Find(acc[i].AccountID);
                    statList.Add(statt);
                }
            }

            else
            {
                var user1 = db.Statses.Find(acc[position - 1].AccountID);
                var user2 = db.Statses.Find(acc[position + 1].AccountID);
                var user3 = db.Statses.Find(acc[position + 2].AccountID);

                statList.Add(user1);
                statList.Add(user2);
                statList.Add(user3);
            }

            return statList;
        }


        public static List<Stats> InitializeBattle(int userId, int opponentId)
        {
            GameContext db = new GameContext();

            var userStats = db.Statses.Find(userId);
            var opponentStats = db.Statses.Find(opponentId);


            List<Stats> statList = new List<Stats>() { userStats, opponentStats };


            //moze zwrocic liste postaci, 1 i 2 postac



            /*
              
             
            jesli agi1>agi2 itp
            (agi1-agi2)*10
            itp


            

             */





            //mamy 6 statow
            /*


            jesli woj:
            sile
            szczecie
            wytrzymalosc









            zwinnosc?
            zrecznosc
            inteligencja
            szczescie
            wytrzymalosc?
            zywotnosc?

            userStats.Agi;
            userStats.Dex;
            userStats.Int;
            userStats.Luk;
            userStats.Str;
            userStats.Vit;
            
            */


            return statList;
        }

        public bool CheckQuests(Account user, int id)
        {
            GameContext db = new GameContext();
            var quest = db.AccountQuestes.Single(x => x.AccountId == user.AccountID);

            if (id == 0) quest.QuestActive = quest.Quest1;
            else if (id == 1) quest.QuestActive = quest.Quest2;
            else if (id == 2) quest.QuestActive = quest.Quest3;

        //    quest.

            return true;
        }


        public static bool CheckIfQuest(Account user)
        {
            GameContext db = new GameContext();
            var quest = db.AccountQuestes.Single(x => x.AccountId == user.AccountID);

            if (quest.QuestActive == null) return false;
            else return true;
        }

    }
}