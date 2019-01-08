namespace Projekt_Dejtingsida.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Projekt_Dejtingsida.Models.ProfileDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Projekt_Dejtingsida.Models.ProfileDbContext";
        }

        protected override void Seed(Projekt_Dejtingsida.Models.ProfileDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
