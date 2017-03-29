using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class Guard
    {
        [Key]
        public int GuardID { get; set; }
        public int AccountID { get; set; }
        public DateTime GuardEndTime { get; set; }
    }
}