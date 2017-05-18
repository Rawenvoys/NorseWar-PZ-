namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _30 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatsBoost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Str = c.Int(nullable: false),
                        Agi = c.Int(nullable: false),
                        Dex = c.Int(nullable: false),
                        Vit = c.Int(nullable: false),
                        Int = c.Int(nullable: false),
                        Luk = c.Int(nullable: false),
                        StatsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StatsBoost");
        }
    }
}
