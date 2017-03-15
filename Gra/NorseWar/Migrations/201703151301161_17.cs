namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Message", "Recipent_AccountID", "dbo.Account");
            DropForeignKey("dbo.Message", "Sender_AccountID", "dbo.Account");


            DropIndex("dbo.Message", new[] { "Recipent_AccountID" });
            DropIndex("dbo.Message", new[] { "Sender_AccountID" });
            AddColumn("dbo.Message", "RecipentId", c => c.Int(nullable: false));
            DropColumn("dbo.Message", "RecpientId");
            DropColumn("dbo.Message", "Recipent_AccountID");
            DropColumn("dbo.Message", "Sender_AccountID");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AccountMessage",
                c => new
                    {
                        AccountMessageID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        MessageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountMessageID);
            
            AddColumn("dbo.Message", "Sender_AccountID", c => c.Int());
            AddColumn("dbo.Message", "Recipent_AccountID", c => c.Int());
            AddColumn("dbo.Message", "RecpientId", c => c.Int(nullable: false));
            DropColumn("dbo.Message", "RecipentId");
            CreateIndex("dbo.Message", "Sender_AccountID");
            CreateIndex("dbo.Message", "Recipent_AccountID");
            CreateIndex("dbo.AccountMessage", "MessageID");
            CreateIndex("dbo.AccountMessage", "AccountID");
            AddForeignKey("dbo.AccountMessage", "MessageID", "dbo.Message", "MessageId", cascadeDelete: true);
            AddForeignKey("dbo.Message", "Sender_AccountID", "dbo.Account", "AccountID");
            AddForeignKey("dbo.Message", "Recipent_AccountID", "dbo.Account", "AccountID");
            AddForeignKey("dbo.AccountMessage", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
        }
    }
}
