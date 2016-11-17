namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountFriend",
                c => new
                    {
                        AccountFriendID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        FriendID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountFriendID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.Friend", t => t.FriendID, cascadeDelete: true)
                .Index(t => t.AccountID)
                .Index(t => t.FriendID);
            
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        FriendID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.FriendID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountFriend", "FriendID", "dbo.Friend");
            DropForeignKey("dbo.AccountFriend", "AccountID", "dbo.Account");
            DropIndex("dbo.AccountFriend", new[] { "FriendID" });
            DropIndex("dbo.AccountFriend", new[] { "AccountID" });
            DropTable("dbo.Friend");
            DropTable("dbo.AccountFriend");
        }
    }
}
