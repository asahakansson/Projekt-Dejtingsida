namespace Projekt_Dejtingsida.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixatID : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.FriendRequestModels");
            DropPrimaryKey("dbo.FriendModels");
            DropColumn("dbo.FriendRequestModels", "RequestID");
            DropColumn("dbo.FriendModels", "ContactId");
            AddColumn("dbo.FriendRequestModels", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.FriendModels", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.FriendRequestModels", "Id");
            AddPrimaryKey("dbo.FriendModels", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendModels", "ContactId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.FriendRequestModels", "RequestID", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.FriendModels");
            DropPrimaryKey("dbo.FriendRequestModels");
            DropColumn("dbo.FriendModels", "Id");
            DropColumn("dbo.FriendRequestModels", "Id");
            AddPrimaryKey("dbo.FriendModels", "ContactId");
            AddPrimaryKey("dbo.FriendRequestModels", "RequestID");
        }
    }
}
