namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _37 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StatsBoost", new[] { "Id" });
            DropPrimaryKey("dbo.StatsBoost");
            AlterColumn("dbo.StatsBoost", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.StatsBoost", "Id");
            CreateIndex("dbo.StatsBoost", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StatsBoost", new[] { "Id" });
            DropPrimaryKey("dbo.StatsBoost");
            AlterColumn("dbo.StatsBoost", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.StatsBoost", "Id");
            CreateIndex("dbo.StatsBoost", "Id");
        }
    }
}
