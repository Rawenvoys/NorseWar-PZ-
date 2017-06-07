using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class Battle
    {
        public double Player1Hp { get; set; }
        public double Player2Hp { get; set; }
        public Account enemy { get; set; }
        public List<Damage> Hits { get; set; }

        public Battle(double Player1Hp, double Player2Hp, Account enemy)
        {
            this.Player1Hp = Player1Hp;
            this.Player2Hp = Player2Hp;
            this.enemy = enemy;
            this.Hits = new List<Damage>();
        }
    }

    public class Damage
    {
        public double Player1Dmg { get; set; }
        public double Player2Dmg { get; set; }
        public Damage(double Player1Dmg, double Player2Dmg)
        {
            this.Player1Dmg = Player1Dmg;
            this.Player2Dmg = Player2Dmg;
        }
    }
}