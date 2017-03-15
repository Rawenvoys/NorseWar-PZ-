namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Message", "SenderId", c => c.Int(nullable: false));
            AddColumn("dbo.Message", "RecpientId", c => c.Int(nullable: false));
            AddColumn("dbo.Message", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Message", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Message", "Recipent_AccountID", c => c.Int());
            AddColumn("dbo.Message", "Sender_AccountID", c => c.Int());
            CreateIndex("dbo.Message", "Recipent_AccountID");
            CreateIndex("dbo.Message", "Sender_AccountID");
            AddForeignKey("dbo.Message", "Recipent_AccountID", "dbo.Account", "AccountID");
            AddForeignKey("dbo.Message", "Sender_AccountID", "dbo.Account", "AccountID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "Sender_AccountID", "dbo.Account");
            DropForeignKey("dbo.Message", "Recipent_AccountID", "dbo.Account");
            DropIndex("dbo.Message", new[] { "Sender_AccountID" });
            DropIndex("dbo.Message", new[] { "Recipent_AccountID" });
            DropColumn("dbo.Message", "Sender_AccountID");
            DropColumn("dbo.Message", "Recipent_AccountID");
            DropColumn("dbo.Message", "Status");
            DropColumn("dbo.Message", "Date");
            DropColumn("dbo.Message", "RecpientId");
            DropColumn("dbo.Message", "SenderId");
        }
    }
}
