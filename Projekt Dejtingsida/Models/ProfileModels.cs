using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt_Dejtingsida.Models
{
    public class Profile
    {
        [Key]
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfileURL { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}