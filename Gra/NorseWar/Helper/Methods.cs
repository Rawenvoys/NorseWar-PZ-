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


        public static void StartGuard(int id)
        {
            GameContext db = new GameContext();
            var user = db.Accounts.Find(id);
            Guard guar = new Guard();
            guar.AccountID = id;
            guar.GuardEndTime = DateTime.Now.AddDays(2);
            db.Quards.Add(guar);
            db.SaveChanges();
        }


        public static void AddPoint(int id, Account user)
        {
            GameContext db = new GameContext();
            var baseUser = db.Accounts.Find(user.AccountID);

            switch (id)
            {
                case 0:
                    baseUser.Stats.Str++;
                    db.SaveChanges();
                    break;

                case 1:
                    baseUser.Stats.Agi++;
                    db.SaveChanges();
                    break;

                case 2:
                    baseUser.Stats.Vit++;
                    db.SaveChanges();
                    break;

                case 3:
                    baseUser.Stats.Dex++;
                    db.SaveChanges();
                    break;

                case 4:
                    baseUser.Stats.Int++;
                    db.SaveChanges();
                    break;

                case 5:
                    baseUser.Stats.Luk++;
                    db.SaveChanges();
                    break;
            }
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

    }
}