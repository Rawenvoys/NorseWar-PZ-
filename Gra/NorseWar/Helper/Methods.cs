using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorseWar.Models;
using NorseWar.Models.DAL;
using System.Web.Mvc;
using NorseWar.Models.BattleModel;

namespace NorseWar.Helper
{
    public class Methods
    {
        public static string RegisterSuccess { get; set; }  //powodzenie rejestracji
        public static string LoginFailed { get; set; }   //nie ma takiego konta
        public static string AccountActive { get; set; }   //konto zajete


        public static void AddStatsBonus(Account user)
        {
            GameContext db = new GameContext();
            bool exist = db.StatsBoosts.Any(x => x.AccountId == user.AccountID);
            if (!exist)
            {
                StatsBoost boost = new StatsBoost() { Agi = 0, Dex = 0, Int = 0, Luk = 0, Str = 0, Vit = 0, AccountId = user.AccountID };
                db.StatsBoosts.Add(boost);
                db.SaveChanges();
            }
        }

        public static void AddItemsToToNewUser(Account user)
        {
            GameContext db = new GameContext();
            var size = db.Backpacks.Where(x => x.AccountId == user.AccountID && x.Equiped == true).ToList();
            if (size == null || size.Count < 4)
            {
                if (size.Count > 0)
                {
                    for (int i = 0; i < size.Count; i++)
                    {
                        var back = db.Backpacks.Find(size[i].Id);
                        db.Backpacks.Remove(back);
                    }
                    db.SaveChanges();
                }

                var ItemList = db.Items.ToList();
                var oko = ItemList;
                for (int i = 0; i < 4; i++)
                {
                    Backpack back = new Backpack() { AccountId = user.AccountID, Equiped = true, ItemId = ItemList[i].Id };
                    db.Backpacks.Add(back);
                }
                db.SaveChanges();
                //1,2,3,5
            }
        }


        public static List<Quest> GetRandomQuest(int uId)
        {
            GameContext db = new GameContext();
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


        public static List<Quest> GetRandomQuestAfterFinish(int uId, int resultId)
        {
            GameContext db = new GameContext();
            Random rand = new Random();
            var questsList = db.Quests.ToList();
            var questOrder = questsList.OrderBy(x => rand.Next(questsList.Count)).Take(3).ToList();

            var aq = db.AccountQuestes.Find(resultId);
            aq.Quest1 = questOrder[0].Id;
            aq.Quest2 = questOrder[1].Id;
            aq.Quest3 = questOrder[2].Id;
            db.SaveChanges();
            return questOrder;
        }


        public static List<Quest> ShowQuestions(int uId)
        {
            GameContext db = new GameContext();
            var check = db.AccountQuestes.Any(x => x.AccountId == uId);
            if (check)
            {
                var result = db.AccountQuestes.Single(x => x.AccountId == uId);
                if (result.Quest1 == null && result.QuestActive == null)
                {
                    int resultId = result.Id;
                    return GetRandomQuestAfterFinish(uId, resultId);
                }
                else
                {
                    var q1 = db.Quests.Find(result.Quest1);
                    var q2 = db.Quests.Find(result.Quest2);
                    var q3 = db.Quests.Find(result.Quest3);

                    List<Quest> list = new List<Quest> { q1, q2, q3 };
                    return list;
                }
            }
            else
            {
                return GetRandomQuest(uId);
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
            return getUser + 1;
        }


        public static Account ShowUserFromPosition(int position)
        {
            GameContext db = new GameContext();
            List<Account> acc = db.Accounts.OrderByDescending(x => x.Experience).ToList();

            //JESLI POZYCJA WIEKSZA NIZ ILOSC ZIOMKOW TO DAC INFO ZE NIE MA TYLU GRACZY XD

            if (position > acc.Count)
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


        public static Tuple<Account, Account> GetUsers(Account user)
        {
            GameContext db = new GameContext();
            List<Account> acc = db.Accounts.OrderByDescending(x => x.Experience).ToList();
            int getUser = acc.FindIndex(x => x.AccountID == user.AccountID);
            int higherUserID;
            int lowerUserID;
            if (getUser == 0)
            {
                higherUserID = getUser + 1;
                lowerUserID = getUser + 2;
            }
            else if (getUser == acc.Count - 1)
            {
                higherUserID = getUser - 2;
                lowerUserID = getUser - 1;
            }
            else
            {
                higherUserID = getUser + 1;
                lowerUserID = getUser - 1;
            }

            Account higherAccount = acc[higherUserID];
            Account lowerAccount = acc[lowerUserID];
            Tuple<Account, Account> accounts = new Tuple<Account, Account>(higherAccount, lowerAccount);
            return accounts;
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
            mm = (minute < 10) ? "0" + minute : minute.ToString();

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

        public static void CancelTavern(Account user)
        {
            GameContext db = new GameContext();
            var mission = db.AccountQuestes.Single(x => x.AccountId == user.AccountID);
            mission.EndQuesst = null;
            mission.Quest1 = null;
            mission.Quest2 = null;
            mission.Quest3 = null;
            mission.QuestActive = null;
            mission.StartQuest = null;
            // var guard = db.Quards.Single(x => x.AccountID == user.AccountID);
            // db.Quards.Remove(guard);
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
            int[] arr = { 0, 0, 0 };
            arr[0] = (int)startTime;
            arr[1] = (int)left;
            arr[2] = (int)endTime;
            if (startTime > endTime)
            {
                var time = new TimeSpan(23, 59, 59);
                var newEndTime = time.TotalSeconds;
                var newStartTime = startTime - endTime;
                var newNow = newStartTime;
                if (now < endTime)
                {
                    newNow = endTime - now;
                }
                else
                {
                    newNow = (time.TotalSeconds - now) + (endTime - new TimeSpan(0, 0, 0).TotalSeconds);
                }

                arr[0] = (int)newStartTime;
                arr[1] = (int)newNow;
                arr[2] = (int)newEndTime;
            }
            return arr;
        }


        public static int[] ShowQuestEndTime(Account user)
        {
            GameContext db = new GameContext();
            var quest = db.AccountQuestes.Single(x => x.AccountId == user.AccountID);
            var endTime = quest.EndQuesst.Value.TimeOfDay.TotalSeconds;
            var startTime = quest.StartQuest.Value.TimeOfDay.TotalSeconds;
            var now = DateTime.Now.TimeOfDay.TotalSeconds;
            var left = endTime - now;
            int[] arr = { 0, 0, 0 };
            arr[0] = (int)startTime;
            arr[1] = (int)left;
            arr[2] = (int)endTime;
            if (startTime > endTime)
            {
                var time = new TimeSpan(23, 59, 59);
                var newEndTime = time.TotalSeconds;
                var newStartTime = startTime - endTime;
                var newNow = newStartTime;
                if (now < endTime)
                {
                    newNow = endTime - now;
                }
                else
                {
                    newNow = (time.TotalSeconds - now) + (endTime - new TimeSpan(0, 0, 0).TotalSeconds);
                }

                arr[0] = (int)newStartTime;
                arr[1] = (int)newNow;
                arr[2] = (int)newEndTime;
            }
            return arr;
        }

        public static int getBoost(string name, Account user)
        {
            GameContext db = new GameContext();
            var baseUser = db.StatsBoosts.SingleOrDefault(x => x.AccountId == user.AccountID);
            int elo = 0;
            switch (name)
            {
                case "str":
                    elo = baseUser.Str;
                    break;
                case "agi":
                    elo = baseUser.Agi;
                    break;
                case "vit":
                    elo = baseUser.Vit;
                    break;
                case "dex":
                    elo = baseUser.Dex;
                    break;
                case "int":
                    elo = baseUser.Int;
                    break;
                case "luk":
                    elo = baseUser.Luk;
                    break;
            }
            return elo;
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


        public static void SelectOneQuest(Account user, int id)
        {
            GameContext db = new GameContext();
            var quest = db.AccountQuestes.Single(x => x.AccountId == user.AccountID);

            if (id == 0) quest.QuestActive = quest.Quest1;
            else if (id == 1) quest.QuestActive = quest.Quest2;
            else if (id == 2) quest.QuestActive = quest.Quest3;

            quest.StartQuest = DateTime.Now;
            var que1 = db.Quests.Find(quest.QuestActive);

            quest.EndQuesst = DateTime.Now.AddSeconds(que1.TimeSecond);

            quest.Quest1 = null;
            quest.Quest2 = null;
            quest.Quest3 = null;

            db.SaveChanges();
        }


        public static int SetExp(int lvl, int time)
        {
            Random rand = new Random();
            int value = (time < 90) ? 4 : 5;
            return lvl * 4 * value;
        }

        public static void QuestFinish(Account user)
        {
            GameContext db = new GameContext();
            var questAccount = db.AccountQuestes.Single(x => x.AccountId == user.AccountID);
            int id = (int)questAccount.QuestActive;
            var quest = db.Quests.Find(id);

            int questTime = quest.TimeSecond;
            double questExp = quest.ExpValue;

            int lvl = ShowUserLevel(user);
            int exp = SetExp(lvl, questTime);
            int exp2 = (int)((double)exp * questExp);
            var account = db.Accounts.Find(user.AccountID);
            account.Experience += exp2;

            questAccount.StartQuest = null;
            questAccount.EndQuesst = null;
            questAccount.QuestActive = null;
            db.SaveChanges();
        }


        public static string SetQuestTime(int se)
        {
            decimal s = (decimal)se;
            var h = Math.Floor(s / 3600); //Get whole hours
            s -= h * 3600;
            var m = Math.Floor(s / 60); //Get remaining minutes
            s -= m * 60;
            string min = (m < 10) ? "0" + m.ToString() : m.ToString();
            string sec = (s < 10) ? "0" + s.ToString() : s.ToString();

            return min + ":" + sec;//zero padding on minutes and seconds
        }


        public static bool CheckIfQuest(Account user)
        {
            GameContext db = new GameContext();
            var quest = db.AccountQuestes.Single(x => x.AccountId == user.AccountID);

            if (quest.QuestActive == null) return false;
            else return true;
        }


        public static List<Stats> InitializeBattle(int userId, int opponentId)
        {
            GameContext db = new GameContext();

            var userStats = db.Statses.Find(userId);
            var opponentStats = db.Statses.Find(opponentId);

            List<Stats> statList = new List<Stats>() { userStats, opponentStats };

            return statList;
        }


        public static StatsInfo CalculateBattleStats(Account acc)
        {
            GameContext db = new GameContext();
            Account account = db.Accounts.Single(x => x.AccountID == acc.AccountID);
            double dmg = 0;
            double hp = 0;
            if (account.CharacterClass == Characters.Mage)
            {
                dmg = (double)account.Stats.Int * 10 + (double)account.Stats.Str * 0.1 + (double)account.Stats.Agi * 0.2;
                hp = (double)account.Stats.Vit * 7 + (double)account.Stats.Dex * 2;
            }
            else if (account.CharacterClass == Characters.Warrior)
            {
                dmg = (double)account.Stats.Int * 0.1 + (double)account.Stats.Str * 16 + (double)account.Stats.Agi * 0.1;
                hp = (double)account.Stats.Vit * 9 + (double)account.Stats.Dex * 1;
            }
            else if (account.CharacterClass == Characters.Archer)
            {
                dmg = (double)account.Stats.Int * 0.1 + (double)account.Stats.Str * 0.2 + (double)account.Stats.Agi * 9;
                hp = (double)account.Stats.Vit * 5 + (double)account.Stats.Dex * 1.5;
            }
            double crit = ((double)account.Stats.Luk * 1.1 / (double)ShowUserLevel(account)) * 10;
            StatsInfo info = new StatsInfo(account.CharacterClass, hp, dmg, crit);
            return info;
        }


        public static Account PickWinner(Tuple<Account, StatsInfo, Account, StatsInfo> enemies)
        {
            int userRounds = CountRoundToDie(enemies.Item4.Dmg, enemies.Item4.Crit, enemies.Item2.Hp);
            int enemyRounds = CountRoundToDie(enemies.Item2.Dmg, enemies.Item2.Crit, enemies.Item4.Hp);
            if (userRounds < enemyRounds)
                return enemies.Item3;
            else
                return enemies.Item1;
        }


        public static int CountRoundToDie(double dmg, double crit, double hp)
        {
            int count = 0;
            while (true)
            {
                Random r = new Random();
                int critChance = r.Next(100);
                hp = hp - dmg;
                if (crit > critChance)
                {
                    hp = hp - dmg;
                }
                count++;
                if (hp <= 0)
                    return count;
            }
        }


        public static void SendGold(int id1, int id2, int value)
        {
            GameContext db = new GameContext();
            Account account1 = db.Accounts.Find(id1);
            Account account2 = db.Accounts.Find(id2);
            account1.Gold = account1.Gold - value;
            account2.Gold = account2.Gold + value;
            db.SaveChanges();
        }



        //zalozone na siebie itemki
        public static List<Backpack> GetItemsOnUser(int id)
        {
            GameContext db = new GameContext();
            var user = db.Accounts.Find(id);
            return db.Backpacks.Where(x => x.AccountId == user.AccountID && x.Equiped == true).ToList();
        }

        public static List<Backpack> GetItemsOnUserBackpack(Account user)
        {
            GameContext db = new GameContext();
            return db.Backpacks.Where(x => x.AccountId == user.AccountID && x.Equiped == false).ToList();
        }


        //BRONIE NA UZYTKOWNIKU PO ID
        public static int? GetAccessoryId(int id)
        {
            int? result = null;
            foreach (var item in GetItemsOnUser(id))
            {
                if (item.Item.Type == TypeOfItem.Accessory)
                {
                    result = item.ItemId;
                    break;
                }
            }
            return result;
        }



        public static int? GetArmorId(int id)
        {
            int? result = null;
            foreach (var item in GetItemsOnUser(id))
            {
                if (item.Item.Type == TypeOfItem.Armor)
                {
                    result = item.ItemId;
                    break;
                }
            }
            return result;
        }

        public static int? GetBootId(int id)
        {
            int? result = null;
            foreach (var item in GetItemsOnUser(id))
            {
                if (item.Item.Type == TypeOfItem.Boots)
                {
                    result = item.ItemId;
                    break;
                }
            }
            return result;
        }

        public static int? GetHelmetId(int id)
        {
            int? result = null;
            foreach (var item in GetItemsOnUser(id))
            {
                if (item.Item.Type == TypeOfItem.Helmet)
                {
                    result = item.ItemId;
                    break;
                }
            }
            return result;
        }

        public static int? GetLegId(int id)
        {
            int? result = null;
            foreach (var item in GetItemsOnUser(id))
            {
                if (item.Item.Type == TypeOfItem.Legs)
                {
                    result = item.ItemId;
                    break;
                }
            }
            return result;
        }

        public static int? GetShieldId(int id)
        {
            int? result = null;
            foreach (var item in GetItemsOnUser(id))
            {
                if (item.Item.Type == TypeOfItem.Shield)
                {
                    result = item.ItemId;
                    break;
                }
            }
            return result;
        }

        public static int? GetWeaponId(int id)
        {
            int? result = null;
            foreach (var item in GetItemsOnUser(id))
            {
                if (item.Item.Type == TypeOfItem.Weapon)
                {
                    result = item.ItemId;
                    break;
                }
            }
            return result;
        }


        public static Item InfoAboutItem(int id)
        {
            GameContext db = new GameContext();
            return db.Items.Single(x => x.Id == id);
        }

        /*
         1.no to tak, trzeba zrobić metodę dodającą bonus gdy zakładamy itemkę na puste miejsce 
            (w parametrze mogę podawać nazwę, w metodzie trzeba sprawdzić czy taka nazwa itemka 
            jest u usera w plecaku  i jeśli jest to dodać wartość statystyk przedmiotu do boosta)
         */

        public static void DragItem(Account user, int id)
        {
            GameContext db = new GameContext();
            //sprawdzamy czy w plecaku usera istnieje taki itemek
            var item = db.Backpacks.SingleOrDefault(x => x.AccountId == user.AccountID && x.ItemId == id && x.Equiped == false);
            // bool isInBack = db.Backpacks.Any(x => x.AccountId == user.AccountID && x.ItemId == id && x.Equiped == false);

            if (item != null)
            {
                item.Equiped = true;
                var boost = db.StatsBoosts.Single(x => x.AccountId == user.AccountID);
                boost.Str += item.Item.StrBonus;
                boost.Agi += item.Item.AgiBonus;
                boost.Dex += item.Item.DexBonus;
                boost.Vit += item.Item.VitBonus;
                boost.Int += item.Item.IntBonus;
                boost.Luk += item.Item.LukBonus;

                db.SaveChanges();
            }
        }


        //zdejmowanie itemka
        public static void TakeOffEquippedItem(Account user, int id)
        {
            GameContext db = new GameContext();
            //sprawdzamy czy w plecaku usera istnieje taki itemek
            var item = db.Backpacks.SingleOrDefault(x => x.AccountId == user.AccountID && x.ItemId == id && x.Equiped == true);
            // bool isInBack = db.Backpacks.Any(x => x.AccountId == user.AccountID && x.ItemId == id && x.Equiped == false);

            if (item != null)
            {
                item.Equiped = false;
                var boost = db.StatsBoosts.Single(x => x.AccountId == user.AccountID);
                boost.Str -= item.Item.StrBonus;
                boost.Agi -= item.Item.AgiBonus;
                boost.Dex -= item.Item.DexBonus;
                boost.Vit -= item.Item.VitBonus;
                boost.Int -= item.Item.IntBonus;
                boost.Luk -= item.Item.LukBonus;

                db.SaveChanges();
            }
        }


        public static void ChangeItems(Account user, int toBeOn, int toBeOff)
        {
            GameContext db = new GameContext();
            var itemToBeOn = db.Backpacks.SingleOrDefault(x => x.AccountId == user.AccountID && x.ItemId == toBeOn && x.Equiped == false);
            var itemToBeOff = db.Backpacks.SingleOrDefault(x => x.AccountId == user.AccountID && x.ItemId == toBeOff && x.Equiped == true);

            if(itemToBeOn != null && itemToBeOff != null)
            {
                //sprawdzamy ich typy
                var typeItemToOn = db.Items.Find(itemToBeOn.ItemId).Type;
                var typeItemToOff = db.Items.Find(itemToBeOff.ItemId).Type;

                if(typeItemToOn == typeItemToOff)
                {
                    itemToBeOn.Equiped = true;
                    itemToBeOff.Equiped = false;
                    var boost = db.StatsBoosts.Single(x => x.AccountId == user.AccountID);
                    boost.Str = boost.Str - itemToBeOff.Item.StrBonus + itemToBeOn.Item.StrBonus;
                    boost.Agi = boost.Agi - itemToBeOff.Item.AgiBonus + itemToBeOn.Item.AgiBonus;
                    boost.Dex = boost.Dex - itemToBeOff.Item.DexBonus + itemToBeOn.Item.DexBonus;
                    boost.Vit = boost.Vit - itemToBeOff.Item.VitBonus + itemToBeOn.Item.VitBonus;
                    boost.Int = boost.Int - itemToBeOff.Item.IntBonus + itemToBeOn.Item.IntBonus;
                    boost.Luk = boost.Luk - itemToBeOff.Item.LukBonus + itemToBeOn.Item.LukBonus;

                    db.SaveChanges();
                }     
            }
        }


        public static void CreateMarket(Account user, List<Item> list)
        {
            GameContext db = new GameContext();
            Market market = new Market() { AccountId = user.AccountID };
            market.Item1 = list[0].Id;
            market.Item2 = list[1].Id;
            market.Item3 = list[2].Id;
            market.Item4 = list[3].Id;
            market.Item5 = list[4].Id;
            market.Item6 = list[5].Id;
            market.ResetDay = DateTime.Now;

            db.Markets.Add(market);
            db.SaveChanges();
        }


        public static void EditMarket(Account user, List<Item> list)
        {
            GameContext db = new GameContext();
            var market = db.Markets.Single(x => x.AccountId == user.AccountID);
            market.Item1 = list[0].Id;
            market.Item2 = list[1].Id;
            market.Item3 = list[2].Id;
            market.Item4 = list[3].Id;
            market.Item5 = list[4].Id;
            market.Item6 = list[5].Id;
            market.ResetDay = DateTime.Now;
            db.SaveChanges();
        }

        public static List<Item> SetItemToMarket(Account user)
        {
            GameContext db = new GameContext();
            Random rand = new Random();
            var items = db.Items.ToList();
            var randItem = items.OrderBy(x => rand.Next(items.Count)).Take(6).ToList();

            var marketSingle = db.Markets.SingleOrDefault(x => x.AccountId == user.AccountID);
            if (marketSingle != null)
            {
                //jesli minela polnoc to reset
                if (DateTime.Now.Day > marketSingle.ResetDay.Day)
                {
                    var newRandItem = items.OrderBy(x => rand.Next(items.Count)).Take(6).ToList();
                    EditMarket(user, newRandItem);
                    return newRandItem;
                }
                else
                {
                    List<Item> itemList = new List<Item>();
                    itemList.Add(db.Items.Find(marketSingle.Item1));
                    itemList.Add(db.Items.Find(marketSingle.Item2));
                    itemList.Add(db.Items.Find(marketSingle.Item3));
                    itemList.Add(db.Items.Find(marketSingle.Item4));
                    itemList.Add(db.Items.Find(marketSingle.Item5));
                    itemList.Add(db.Items.Find(marketSingle.Item6));
                    return itemList;
                }
            }
            else
            {
                CreateMarket(user, randItem);
                return randItem;
            }
        }



        public static Item BuyItemFromMarket(Account user, int itemId)
        {
            GameContext db = new GameContext();
            Item sendItem = null;

            var market = db.Markets.SingleOrDefault(x => x.AccountId == user.AccountID);
            bool existItem = CheckExistItem(market, itemId);

            int userMoney = user.Gold;
            var item = db.Items.Find(itemId);
            int itemPrice = (item.AgiBonus + item.DexBonus + item.IntBonus + item.LukBonus + item.StrBonus + item.VitBonus) * 4;
            int backpackSpace = db.Backpacks.Where(x => x.AccountId == user.AccountID && x.Equiped == false).ToList().Count;

            if (userMoney > itemPrice && backpackSpace < 6 && existItem == true)
            {
                var acc = db.Accounts.Single(x => x.AccountID == user.AccountID);
                acc.Gold -= itemPrice;
                //wtedy odejmujemy hajs z konta usera, dodajemy do backpacka kupowany itemek, 
                //no i z bazy wypierdala rekord z kupionym itemkiem, ja tam na lokalu sobie usunę diva

                Backpack backpack = new Backpack() { AccountId = user.AccountID, Equiped = false, ItemId = itemId };
                db.Backpacks.Add(backpack);

                List<int> idList = new List<int>();
                idList.Add(market.Item1);
                idList.Add(market.Item2);
                idList.Add(market.Item3);
                idList.Add(market.Item4);
                idList.Add(market.Item5);
                idList.Add(market.Item6);

                var items = db.Items.ToList();

                for(int i = 0; i < items.Count; i++)
                {
                    var itemFromRand = RandItem(items);
                    int randItem = itemFromRand.Id;
                    sendItem = itemFromRand;
                    if (idList.Any(x => x.ToString().Equals(randItem.ToString())))
                        continue;

                    else
                    {
                        DeleteAndReplaceItemFromMarket(market, itemId, randItem);
                        break;
                    }
                }
                db.SaveChanges();
            }  
            
            return sendItem;
        }

        public static void AddRandomItemAfterTavern(Account user)
        {
            GameContext db = new GameContext();
            int backpackSpace = db.Backpacks.Where(x => x.AccountId == user.AccountID && x.Equiped == false).ToList().Count;
            if(backpackSpace < 6)
            {
                Random rand = new Random();
                int randNumber = rand.Next(1,60);
                if(randNumber > 34 && randNumber < 41) // ok 10% szans?
                {
                    Item item = RandItem(db.Items.ToList());
                    Backpack backpack = new Backpack() { AccountId = user.AccountID, Equiped = false, ItemId = item.Id };
                    db.Backpacks.Add(backpack);
                    db.SaveChanges();
                }
            }     
        }


        public static void SellItemToMarket(Account user, int id)
        {
            GameContext db = new GameContext();
            var itemInBackpack = db.Backpacks.SingleOrDefault(x => x.AccountId == user.AccountID && x.Equiped == false && x.ItemId == id);

            if(itemInBackpack != null)
            {
                var item = db.Items.Find(id);
                int itemPrice = (item.AgiBonus + item.DexBonus + item.IntBonus + item.LukBonus + item.StrBonus + item.VitBonus);
                int price = (int)((double)itemPrice * 0.55);

                var account = db.Accounts.Find(user.AccountID);
                account.Gold += price;

                db.Backpacks.Remove(itemInBackpack);
                db.SaveChanges();
            }
        }


        public static Item RandItem(List<Item> items)
        {
            Random rand = new Random();
            return items.OrderBy(x => rand.Next(items.Count)).Take(1).Single();
        }

        public static bool CheckExistItem(Market market, int itemId)
        {
            bool existItem = false;
            if (market.Item1 == itemId)
                existItem = true;
            else if (market.Item2 == itemId)
                existItem = true;
            else if (market.Item3 == itemId)
                existItem = true;
            else if (market.Item4 == itemId)
                existItem = true;
            else if (market.Item5 == itemId)
                existItem = true;
            else if (market.Item6 == itemId)
                existItem = true;

            return existItem;
        }

        public static void DeleteAndReplaceItemFromMarket(Market market, int itemId, int newId)
        {
            if (market.Item1 == itemId)
                market.Item1 = newId;
            else if (market.Item2 == itemId)
                market.Item2 = newId;
            else if (market.Item3 == itemId)
                market.Item3 = newId;
            else if (market.Item4 == itemId)
                market.Item4 = newId;
            else if (market.Item5 == itemId)
                market.Item5 = newId;
            else if (market.Item6 == itemId)
                market.Item6 = newId;
        }
    }
}