namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _39 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StatsBoost", "Id", "dbo.Account");
            DropIndex("dbo.StatsBoost", new[] { "Id" });
            DropPrimaryKey("dbo.StatsBoost");
            AddColumn("dbo.StatsBoost", "AccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.StatsBoost", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.StatsBoost", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.StatsBoost");
            AlterColumn("dbo.StatsBoost", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.StatsBoost", "AccountId");
            AddPrimaryKey("dbo.StatsBoost", "Id");
            CreateIndex("dbo.StatsBoost", "Id");
            AddForeignKey("dbo.StatsBoost", "Id", "dbo.Account", "AccountID");
        }
    }
}
