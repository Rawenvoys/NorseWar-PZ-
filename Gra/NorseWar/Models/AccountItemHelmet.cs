using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class AccountItemHelmet
    {
        public int AccountItemHelmetID { get; set; }
        public int ItemHelmetID { get; set; }
        public int AccountID { get; set; }
        public bool Equiped { get; set; }

        public virtual Account Account { get; set; }
        public virtual ItemHelmet ItemHelmet { get; set; }
    }
}