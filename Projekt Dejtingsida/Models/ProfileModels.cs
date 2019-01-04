﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projekt_Dejtingsida.Models
{
    public class ProfileModels
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
    public class ProfileDbContext : DbContext
    {
        public DbSet<ProfileModels> Profiles { get; set; }
        public ProfileDbContext() : base("ProfileDb") { }
    }
}