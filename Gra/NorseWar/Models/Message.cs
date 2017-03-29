using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorseWar.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int RecipentId { get; set; }

        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}