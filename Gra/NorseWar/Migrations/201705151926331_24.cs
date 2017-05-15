namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.RandomTable");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RandomTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        Quest1Rand = c.Int(),
                        Quest2Rand = c.Int(),
                        Quest3Rand = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
