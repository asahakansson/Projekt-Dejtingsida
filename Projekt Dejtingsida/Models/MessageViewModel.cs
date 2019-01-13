using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt_Dejtingsida.Models
{
    public class MessageViewModel
    {
        public int MessageId { get; set; }
        public string SenderName { get; set; }
        public string Reciever { get; set; }
        public DateTimeOffset SendDate { get; set; }
        public string MessageText { get; set; }

    }
}