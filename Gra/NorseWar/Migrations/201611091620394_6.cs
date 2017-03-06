namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountItemShield",
                c => new
                    {
                        AccountItemShieldID = c.Int(nullable: false, identity: true),
                        ItemShieldID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemShieldID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.ItemShield", t => t.ItemShieldID, cascadeDelete: true)
                .Index(t => t.ItemShieldID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.ItemShield",
                c => new
                    {
                        ItemShieldID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StrBonus = c.Int(nullable: false),
                        AgiBonus = c.Int(nullable: false),
                        DexBonus = c.Int(nullable: false),
                        VitBonus = c.Int(nullable: false),
                        IntBonus = c.Int(nullable: false),
                        LukBonus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemShieldID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountItemShield", "ItemShieldID", "dbo.ItemShield");
            DropForeignKey("dbo.AccountItemShield", "AccountID", "dbo.Account");
            DropIndex("dbo.AccountItemShield", new[] { "AccountID" });
            DropIndex("dbo.AccountItemShield", new[] { "ItemShieldID" });
            DropTable("dbo.ItemShield");
            DropTable("dbo.AccountItemShield");
        }
    }
}
