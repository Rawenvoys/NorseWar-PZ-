using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class ItemHelmet
    {
        public int ItemHelmetID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StrBonus { get; set; }
        public int AgiBonus { get; set; }
        public int DexBonus { get; set; }
        public int VitBonus { get; set; }
        public int IntBonus { get; set; }
        public int LukBonus { get; set; }

        public virtual ICollection<AccountItemHelmet> AccountItemHelmet { get; set; }
    }
}