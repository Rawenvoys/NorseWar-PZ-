namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guard",
                c => new
                    {
                        GuardID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        GuardEndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GuardID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Guard");
        }
    }
}
