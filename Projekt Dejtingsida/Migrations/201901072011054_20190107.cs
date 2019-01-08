namespace Projekt_Dejtingsida.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190107 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendRequestModels",
                c => new
                    {
                        RequestID = c.String(nullable: false, maxLength: 128),
                        Person1 = c.String(),
                        Person2 = c.String(),
                    })
                .PrimaryKey(t => t.RequestID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FriendRequestModels");
        }
    }
}
