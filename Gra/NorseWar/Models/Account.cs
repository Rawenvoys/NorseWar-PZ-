using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Gold { get; set; }
        public int Experience { get; set; }
        public string Mail { get; set; }
        public string CharacterClass { get; set; }
  
        public virtual Stats Stats { get; set; }

        public virtual ICollection<AccountItemHelmet> AccountItemHelmet { get; set; }
        public virtual ICollection<AccountItemShield> AccountItemShield { get; set; }
        public virtual ICollection<AccountItemArmor> AccountItemArmor { get; set; }
        public virtual ICollection<AccountItemWeapon> AccountItemWeapon { get; set; }
        public virtual ICollection<AccountItemLegs> AccountItemLegs { get; set; }
        public virtual ICollection<AccountItemAccessory> AccountItemAccessory { get; set; }
        public virtual ICollection<AccountItemBoots> AccountItemBoots { get; set; }


        public virtual ICollection<AccountMessage> AccountMessage { get; set; }
        public virtual ICollection<AccountFriend> AccoundFriend { get; set; }
    }
}