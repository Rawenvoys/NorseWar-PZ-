namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Backpack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StrBonus = c.Int(nullable: false),
                        AgiBonus = c.Int(nullable: false),
                        DexBonus = c.Int(nullable: false),
                        VitBonus = c.Int(nullable: false),
                        IntBonus = c.Int(nullable: false),
                        LukBonus = c.Int(nullable: false),
                        Url = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Backpack", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Backpack", "AccountId", "dbo.Account");
            DropIndex("dbo.Backpack", new[] { "AccountId" });
            DropIndex("dbo.Backpack", new[] { "ItemId" });
            DropTable("dbo.Item");
            DropTable("dbo.Backpack");
        }
    }
}
