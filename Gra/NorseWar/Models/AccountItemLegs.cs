using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class AccountItemLegs
    {
        public int AccountItemLegsID { get; set; }
        public int ItemLegsID { get; set; }
        public int AccountID { get; set; }
        public bool Equiped { get; set; }

        public virtual Account Account { get; set; }
        public virtual ItemLegs ItemLegs { get; set; }
    }
}