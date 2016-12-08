using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models.ViewModel
{
    public class ItemViewModel
    {
        public List<ItemAccessory> accessories = new List<ItemAccessory>();
        public List<ItemArmor> armors = new List<ItemArmor>();
        public List<ItemBoots> boots = new List<ItemBoots>();
        public List<ItemHelmet> helmets = new List<ItemHelmet>();
        public List<ItemLegs> legss = new List<ItemLegs>();
        public List<ItemShield> shields = new List<ItemShield>();
        public List<ItemWeapon> weapons = new List<ItemWeapon>();
    }
}