namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountMessage",
                c => new
                    {
                        AccountMessageID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        MessageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountMessageID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.Message", t => t.MessageID, cascadeDelete: true)
                .Index(t => t.AccountID)
                .Index(t => t.MessageID);
            
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Gold = c.Int(nullable: false),
                        Mail = c.String(),
                        AccountMessageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountID);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        AccountMessageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountMessage", "MessageID", "dbo.Message");
            DropForeignKey("dbo.AccountMessage", "AccountID", "dbo.Account");
            DropIndex("dbo.AccountMessage", new[] { "MessageID" });
            DropIndex("dbo.AccountMessage", new[] { "AccountID" });
            DropTable("dbo.Message");
            DropTable("dbo.Account");
            DropTable("dbo.AccountMessage");
        }
    }
}
