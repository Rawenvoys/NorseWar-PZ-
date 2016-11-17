namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountItemAccessory",
                c => new
                    {
                        AccountItemAccessoryID = c.Int(nullable: false, identity: true),
                        ItemAccessoryID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemAccessoryID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.ItemAccessory", t => t.ItemAccessoryID, cascadeDelete: true)
                .Index(t => t.ItemAccessoryID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.ItemAccessory",
                c => new
                    {
                        ItemAccessoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StrBonus = c.Int(nullable: false),
                        AgiBonus = c.Int(nullable: false),
                        DexBonus = c.Int(nullable: false),
                        VitBonus = c.Int(nullable: false),
                        IntBonus = c.Int(nullable: false),
                        LukBonus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemAccessoryID);
            
            CreateTable(
                "dbo.AccountItemArmor",
                c => new
                    {
                        AccountItemArmorID = c.Int(nullable: false, identity: true),
                        ItemArmorID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemArmorID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.ItemArmor", t => t.ItemArmorID, cascadeDelete: true)
                .Index(t => t.ItemArmorID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.ItemArmor",
                c => new
                    {
                        ItemArmorID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StrBonus = c.Int(nullable: false),
                        AgiBonus = c.Int(nullable: false),
                        DexBonus = c.Int(nullable: false),
                        VitBonus = c.Int(nullable: false),
                        IntBonus = c.Int(nullable: false),
                        LukBonus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemArmorID);
            
            CreateTable(
                "dbo.AccountItemBoots",
                c => new
                    {
                        AccountItemBootsID = c.Int(nullable: false, identity: true),
                        ItemBootsID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemBootsID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.ItemBoots", t => t.ItemBootsID, cascadeDelete: true)
                .Index(t => t.ItemBootsID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.ItemBoots",
                c => new
                    {
                        ItemBootsID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StrBonus = c.Int(nullable: false),
                        AgiBonus = c.Int(nullable: false),
                        DexBonus = c.Int(nullable: false),
                        VitBonus = c.Int(nullable: false),
                        IntBonus = c.Int(nullable: false),
                        LukBonus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemBootsID);
            
            CreateTable(
                "dbo.AccountItemHelmet",
                c => new
                    {
                        AccountItemHelmetID = c.Int(nullable: false, identity: true),
                        ItemHelmetID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemHelmetID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.ItemHelmet", t => t.ItemHelmetID, cascadeDelete: true)
                .Index(t => t.ItemHelmetID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.ItemHelmet",
                c => new
                    {
                        ItemHelmetID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StrBonus = c.Int(nullable: false),
                        AgiBonus = c.Int(nullable: false),
                        DexBonus = c.Int(nullable: false),
                        VitBonus = c.Int(nullable: false),
                        IntBonus = c.Int(nullable: false),
                        LukBonus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemHelmetID);
            
            CreateTable(
                "dbo.AccountItemLegs",
                c => new
                    {
                        AccountItemLegsID = c.Int(nullable: false, identity: true),
                        ItemLegsID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemLegsID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.ItemLegs", t => t.ItemLegsID, cascadeDelete: true)
                .Index(t => t.ItemLegsID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.ItemLegs",
                c => new
                    {
                        ItemLegsID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StrBonus = c.Int(nullable: false),
                        AgiBonus = c.Int(nullable: false),
                        DexBonus = c.Int(nullable: false),
                        VitBonus = c.Int(nullable: false),
                        IntBonus = c.Int(nullable: false),
                        LukBonus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemLegsID);
            
            CreateTable(
                "dbo.AccountItemWeapon",
                c => new
                    {
                        AccountItemWeaponID = c.Int(nullable: false, identity: true),
                        ItemWeaponID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemWeaponID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.ItemWeapon", t => t.ItemWeaponID, cascadeDelete: true)
                .Index(t => t.ItemWeaponID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.ItemWeapon",
                c => new
                    {
                        ItemWeaponID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Character = c.String(),
                        StrBonus = c.Int(nullable: false),
                        AgiBonus = c.Int(nullable: false),
                        DexBonus = c.Int(nullable: false),
                        VitBonus = c.Int(nullable: false),
                        IntBonus = c.Int(nullable: false),
                        LukBonus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemWeaponID);
            
            AddColumn("dbo.Account", "Experience", c => c.Int(nullable: false));
            AddColumn("dbo.Account", "CharacterClass", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountItemWeapon", "ItemWeaponID", "dbo.ItemWeapon");
            DropForeignKey("dbo.AccountItemWeapon", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemLegs", "ItemLegsID", "dbo.ItemLegs");
            DropForeignKey("dbo.AccountItemLegs", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemHelmet", "ItemHelmetID", "dbo.ItemHelmet");
            DropForeignKey("dbo.AccountItemHelmet", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemBoots", "ItemBootsID", "dbo.ItemBoots");
            DropForeignKey("dbo.AccountItemBoots", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemArmor", "ItemArmorID", "dbo.ItemArmor");
            DropForeignKey("dbo.AccountItemArmor", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemAccessory", "ItemAccessoryID", "dbo.ItemAccessory");
            DropForeignKey("dbo.AccountItemAccessory", "AccountID", "dbo.Account");
            DropIndex("dbo.AccountItemWeapon", new[] { "AccountID" });
            DropIndex("dbo.AccountItemWeapon", new[] { "ItemWeaponID" });
            DropIndex("dbo.AccountItemLegs", new[] { "AccountID" });
            DropIndex("dbo.AccountItemLegs", new[] { "ItemLegsID" });
            DropIndex("dbo.AccountItemHelmet", new[] { "AccountID" });
            DropIndex("dbo.AccountItemHelmet", new[] { "ItemHelmetID" });
            DropIndex("dbo.AccountItemBoots", new[] { "AccountID" });
            DropIndex("dbo.AccountItemBoots", new[] { "ItemBootsID" });
            DropIndex("dbo.AccountItemArmor", new[] { "AccountID" });
            DropIndex("dbo.AccountItemArmor", new[] { "ItemArmorID" });
            DropIndex("dbo.AccountItemAccessory", new[] { "AccountID" });
            DropIndex("dbo.AccountItemAccessory", new[] { "ItemAccessoryID" });
            DropColumn("dbo.Account", "CharacterClass");
            DropColumn("dbo.Account", "Experience");
            DropTable("dbo.ItemWeapon");
            DropTable("dbo.AccountItemWeapon");
            DropTable("dbo.ItemLegs");
            DropTable("dbo.AccountItemLegs");
            DropTable("dbo.ItemHelmet");
            DropTable("dbo.AccountItemHelmet");
            DropTable("dbo.ItemBoots");
            DropTable("dbo.AccountItemBoots");
            DropTable("dbo.ItemArmor");
            DropTable("dbo.AccountItemArmor");
            DropTable("dbo.ItemAccessory");
            DropTable("dbo.AccountItemAccessory");
        }
    }
}
