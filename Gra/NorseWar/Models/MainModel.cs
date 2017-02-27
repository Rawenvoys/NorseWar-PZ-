using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class MainModel
    {
        public List<Account> Account { get; set; }
        public Helper.SearchUser SearchUser { get; set; }
    }
}