namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _43 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Market",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        Item1 = c.Int(nullable: false),
                        Item2 = c.Int(nullable: false),
                        Item3 = c.Int(nullable: false),
                        Item4 = c.Int(nullable: false),
                        Item5 = c.Int(nullable: false),
                        Item6 = c.Int(nullable: false),
                        ResetDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Market");
        }
    }
}
