using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekt_Dejtingsida.Models
{
    public class MessageModel
    {
        [Key]
        public int MessageId { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }

        public DateTimeOffset SendDate { get; set; }
        public string MessageText { get; set; }
         
    }
}