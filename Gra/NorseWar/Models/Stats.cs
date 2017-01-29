using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class Stats
    {
        [Key, ForeignKey("Account")]
        public int StatsID { get; set; }
        public int Str { get; set; }
        public int Agi { get; set; }
        public int Dex { get; set; }
        public int Vit { get; set; }
        public int Int { get; set; }
        public int Luk { get; set; }

        public virtual Account Account { get; set; }

        public Stats() { }

        public Stats(Account _account, string className)
        {
            if (className == "Mage")
            {
                Str = 1;
                Agi = 1;
                Dex = 2;
                Vit = 1;
                Int = 5;
                Luk = 1;
            }
            else
            if (className == "Warrior")
            {
                Str = 4;
                Agi = 2;
                Dex = 1;
                Vit = 2;
                Int = 1;
                Luk = 1;
            }
            else
            if (className == "Archer")
            {
                Str = 1;
                Agi = 3;
                Dex = 4;
                Vit = 1;
                Int = 1;
                Luk = 1;
            }
            Account = _account;
        }
    }
}