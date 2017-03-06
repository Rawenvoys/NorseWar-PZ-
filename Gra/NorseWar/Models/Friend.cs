using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class Friend
    {
        public int FriendID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AccountFriend> AccountFriend { get; set; }
    }
}