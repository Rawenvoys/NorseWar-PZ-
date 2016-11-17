using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class AccountFriend
    {
        public int AccountFriendID { get; set; }
        public int AccountID { get; set; }
        public int FriendID { get; set; }

        public virtual Account Account { get; set; }
        public virtual Friend Friend { get; set; }
    }
}