using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class AccountItemWeapon
    {
        public int AccountItemWeaponID { get; set; }
        public int ItemWeaponID { get; set; }
        public int AccountID { get; set; }
        public bool Equiped { get; set; }

        public virtual Account Account { get; set; }
        public virtual ItemWeapon ItemWeapon { get; set; }

        public AccountItemWeapon(Account acc, ItemWeapon iW)
        {
            ItemWeaponID = iW.ItemWeaponID;
            AccountID = acc.AccountID;
            Equiped = false;
            Account = acc;
            ItemWeapon = iW;
        }

        public AccountItemWeapon()
        {

        }
    }
}