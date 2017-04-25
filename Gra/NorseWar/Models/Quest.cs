using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string Description { get; set; }    
        public int TimeSecond { get; set; }
        public double ExpValue { get; set; }
    }
}