using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt_Dejtingsida.Models
{
    public class FriendRequestModels
    {
        [Key]
        public string ID { get; set; }
        public string Person1 { get; set; }
        public string Person2 { get; set; }
    }
}