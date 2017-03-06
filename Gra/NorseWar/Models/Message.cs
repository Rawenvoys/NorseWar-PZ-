using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

  
        public virtual ICollection<AccountMessage> AccountMessage { get; set; }
    }
}