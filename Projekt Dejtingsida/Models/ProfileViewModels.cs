﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt_Dejtingsida.Models
{
    public class ProfileViewModels
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfileURL { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}