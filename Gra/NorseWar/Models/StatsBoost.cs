using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class StatsBoost
    {
        [Key]
        public int Id { get; set; }
        public int Str { get; set; }
        public int Agi { get; set; }
        public int Dex { get; set; }
        public int Vit { get; set; }
        public int Int { get; set; }
        public int Luk { get; set; }

        public int StatsId { get; set; }
    }
}