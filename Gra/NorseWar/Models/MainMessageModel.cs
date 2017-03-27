using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class MainMessageModel
    {
        public List<Message> Messages { get; set; }
        public Message OneMessage { get; set; }
    }
}