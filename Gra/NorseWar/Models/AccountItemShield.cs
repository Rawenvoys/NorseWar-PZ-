﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class AccountItemShield
    {
        public int AccountItemShieldID { get; set; }
        public int ItemShieldID { get; set; }
        public int AccountID { get; set; }
        public bool Equiped { get; set; }

        public virtual Account Account { get; set; }
        public virtual ItemShield ItemShield {get; set;}

        public AccountItemShield(Account acc,ItemShield iS)
        {
            ItemShieldID = iS.ItemShieldID;
            AccountID = acc.AccountID;
            Equiped = false;
            Account = acc;
            ItemShield = iS;
        }

        public AccountItemShield()
        {

        }
    }
}