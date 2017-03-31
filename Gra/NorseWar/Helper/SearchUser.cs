using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorseWar.Models;
using NorseWar.Models.DAL;
using NorseWar.Helper;

namespace NorseWar.Helper
{
    public class SearchUser
    {
        public string Text { get; set; }

        public static Account Search(SearchUser searchuser)
        {
            GameContext db = new GameContext();
            Account user = new Account();

            List<string> listOfString = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            int length = searchuser.Text.Length;
            string[] tab = new string[length];
            string position = null;
            int pos = 0;

            for (int i = 0; i < length; i++)
                tab[i] = searchuser.Text[i].ToString();


            for (int i = 0; i < length; i++)
            {
                if (listOfString.Contains(tab[i]))
                    position += tab[i];

                else
                {
                    position = null;
                    break;
                }
            }

            if(position != null)
            {
                pos = Int32.Parse(position);
                user = Methods.ShowUserFromPosition(pos);
            }

            Account account = db.Accounts.SingleOrDefault(u => u.Login == searchuser.Text || u.AccountID == user.AccountID);
            return account;
        }
    }
}