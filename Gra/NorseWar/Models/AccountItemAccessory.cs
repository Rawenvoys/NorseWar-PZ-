using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class AccountItemAccessory
    {
        public int AccountItemAccessoryID { get; set; }
        public int ItemAccessoryID { get; set; }
        public int AccountID { get; set; }
        public bool Equiped { get; set; }

        public virtual Account Account { get; set; }
        public virtual ItemAccessory ItemAccessory { get; set; }
    }
}