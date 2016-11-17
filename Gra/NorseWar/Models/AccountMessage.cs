using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class AccountMessage
    {
        public int AccountMessageID { get; set; }

        public int AccountID { get; set; }
        public virtual Account Account { get; set; }
        public int MessageID { get; set; }
        public virtual Message Message { get; set; }
    }
}