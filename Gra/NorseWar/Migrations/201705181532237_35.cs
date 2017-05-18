namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _35 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StatsBoost", "Id", "dbo.Account");
            DropIndex("dbo.StatsBoost", new[] { "Id" });
            AddColumn("dbo.StatsBoost", "AccountId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StatsBoost", "AccountId");
            CreateIndex("dbo.StatsBoost", "Id");
            AddForeignKey("dbo.StatsBoost", "Id", "dbo.Account", "AccountID");
        }
    }
}
