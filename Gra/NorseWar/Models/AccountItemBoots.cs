using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class AccountItemBoots
    {
        public int AccountItemBootsID { get; set; }
        public int ItemBootsID { get; set; }
        public int AccountID { get; set; }
        public bool Equiped { get; set; }

        public virtual Account Account { get; set; }
        public virtual ItemBoots ItemBoots { get; set; }
    }
}