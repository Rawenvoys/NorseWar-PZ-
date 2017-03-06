using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorseWar.Models;
using NorseWar.Models.DAL;

namespace NorseWar.Helper
{
    public class SearchUser
    {
        public string Text { get; set; }

        public static Account Search(SearchUser searchuser)
        {
            GameContext db = new GameContext();
            Account acc = db.Accounts.SingleOrDefault(u => u.Login == searchuser.Text);
            return acc;
        }
    }
}