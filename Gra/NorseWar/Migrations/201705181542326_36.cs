namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _36 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.StatsBoost");
            AlterColumn("dbo.StatsBoost", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.StatsBoost", "Id");
            CreateIndex("dbo.StatsBoost", "Id");
            AddForeignKey("dbo.StatsBoost", "Id", "dbo.Account", "AccountID");
            DropColumn("dbo.StatsBoost", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StatsBoost", "AccountId", c => c.Int(nullable: false));
            DropForeignKey("dbo.StatsBoost", "Id", "dbo.Account");
            DropIndex("dbo.StatsBoost", new[] { "Id" });
            DropPrimaryKey("dbo.StatsBoost");
            AlterColumn("dbo.StatsBoost", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.StatsBoost", "Id");
        }
    }
}
