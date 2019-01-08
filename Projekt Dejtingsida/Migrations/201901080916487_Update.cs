namespace Projekt_Dejtingsida.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProfileModels", newName: "Profiles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Profiles", newName: "ProfileModels");
        }
    }
}
    