using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class Backpack
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int AccountId { get; set; }
        public bool Equiped { get; set; }

        public virtual Account Account { get; set; }
        public virtual Item Item { get; set; }

        public Backpack(Account acc, Item item)
        {
            ItemId = item.Id;
            AccountId = acc.AccountID;
            Equiped = false;
            Account = acc;
            Item = item;
        }

        public Backpack()
        {

        }
    }
}
