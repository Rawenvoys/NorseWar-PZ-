namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stats",
                c => new
                    {
                        StatsID = c.Int(nullable: false),
                        Str = c.Int(nullable: false),
                        Agi = c.Int(nullable: false),
                        Dex = c.Int(nullable: false),
                        Vit = c.Int(nullable: false),
                        Int = c.Int(nullable: false),
                        Luk = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatsID)
                .ForeignKey("dbo.Account", t => t.StatsID)
                .Index(t => t.StatsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stats", "StatsID", "dbo.Account");
            DropIndex("dbo.Stats", new[] { "StatsID" });
            DropTable("dbo.Stats");
        }
    }
}
