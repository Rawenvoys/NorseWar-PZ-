namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Message", "Recipent_AccountID", "dbo.Account");
            DropForeignKey("dbo.Message", "Sender_AccountID", "dbo.Account");
            DropIndex("dbo.Message", new[] { "Recipent_AccountID" });
            DropIndex("dbo.Message", new[] { "Sender_AccountID" });
            DropColumn("dbo.Message", "SenderId");
            DropColumn("dbo.Message", "RecipientId");
            DropColumn("dbo.Message", "Date");
            DropColumn("dbo.Message", "Status");
            DropColumn("dbo.Message", "Recipent_AccountID");
            DropColumn("dbo.Message", "Sender_AccountID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Message", "Sender_AccountID", c => c.Int());
            AddColumn("dbo.Message", "Recipent_AccountID", c => c.Int());
            AddColumn("dbo.Message", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Message", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Message", "RecipientId", c => c.Int(nullable: false));
            AddColumn("dbo.Message", "SenderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Message", "Sender_AccountID");
            CreateIndex("dbo.Message", "Recipent_AccountID");
            AddForeignKey("dbo.Message", "Sender_AccountID", "dbo.Account", "AccountID");
            AddForeignKey("dbo.Message", "Recipent_AccountID", "dbo.Account", "AccountID");
        }
    }
}
