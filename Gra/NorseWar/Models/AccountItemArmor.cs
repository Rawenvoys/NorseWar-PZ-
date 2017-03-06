using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class AccountItemArmor
    {
        public int AccountItemArmorID { get; set; }
        public int ItemArmorID { get; set; }
        public int AccountID { get; set; }
        public bool Equiped { get; set; }

        public virtual Account Account { get; set; }
        public virtual ItemArmor ItemArmor { get; set; }
    }
}