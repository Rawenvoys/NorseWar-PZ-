using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class ItemShield
    {
        public int ItemShieldID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StrBonus { get; set; }
        public int AgiBonus { get; set; }
        public int DexBonus { get; set; }
        public int VitBonus { get; set; }
        public int IntBonus { get; set; }
        public int LukBonus { get; set; }
        public string Url { get; set; }

        [DefaultValue("Shield")]
        public string Type { get; set; }

        public virtual ICollection<AccountItemShield> AccountItemShield { get; set; }

        public ItemShield() { }

    }
}