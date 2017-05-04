using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models.BattleModel
{
    public class StatsInfo
    {
        public Characters CharacterClass { get; set; } 
        public double Hp { get; set; }
        public double Dmg { get; set; }
        public double Crit { get; set; }

        public StatsInfo(Characters CharacterClass, double Hp, double Dmg, double Crit)
        {
            this.CharacterClass = CharacterClass;
            this.Hp = Hp;
            this.Dmg = Dmg;
            this.Crit = Crit;
        }
    }
}