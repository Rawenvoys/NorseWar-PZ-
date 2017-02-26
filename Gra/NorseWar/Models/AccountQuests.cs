using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class AccountQuests
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public int Quest1 { get; set; }
        public int Quest2 { get; set; }
        public int Quest3 { get; set; }
    }
}