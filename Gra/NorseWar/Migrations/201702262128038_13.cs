namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountQuests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        Quest1 = c.Int(nullable: false),
                        Quest2 = c.Int(nullable: false),
                        Quest3 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Quest");
            DropTable("dbo.AccountQuests");
        }
    }
}
