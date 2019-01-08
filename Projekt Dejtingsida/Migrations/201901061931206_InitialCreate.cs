namespace Projekt_Dejtingsida.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageModels",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Sender = c.String(),
                        Reciever = c.String(),
                        SendDate = c.DateTimeOffset(nullable: false, precision: 7),
                        MessageText = c.String(),
                    })
                .PrimaryKey(t => t.MessageId);
            
            CreateTable(
                "dbo.ProfileModels",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        ProfileURL = c.String(),
                        Description = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProfileModels");
            DropTable("dbo.MessageModels");
        }
    }
}
