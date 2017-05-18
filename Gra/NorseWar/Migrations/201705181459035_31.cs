namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _31 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountFriend", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemAccessory", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemAccessory", "ItemAccessoryID", "dbo.ItemAccessory");
            DropForeignKey("dbo.AccountItemArmor", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemArmor", "ItemArmorID", "dbo.ItemArmor");
            DropForeignKey("dbo.AccountItemBoots", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemBoots", "ItemBootsID", "dbo.ItemBoots");
            DropForeignKey("dbo.AccountItemHelmet", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemHelmet", "ItemHelmetID", "dbo.ItemHelmet");
            DropForeignKey("dbo.AccountItemLegs", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemLegs", "ItemLegsID", "dbo.ItemLegs");
            DropForeignKey("dbo.AccountItemShield", "AccountID", "dbo.Account");
            DropForeignKey("dbo.AccountItemShield", "ItemShieldID", "dbo.ItemShield");
            DropForeignKey("dbo.AccountFriend", "FriendID", "dbo.Friend");
            DropIndex("dbo.AccountFriend", new[] { "AccountID" });
            DropIndex("dbo.AccountFriend", new[] { "FriendID" });
            DropIndex("dbo.AccountItemAccessory", new[] { "ItemAccessoryID" });
            DropIndex("dbo.AccountItemAccessory", new[] { "AccountID" });
            DropIndex("dbo.AccountItemArmor", new[] { "ItemArmorID" });
            DropIndex("dbo.AccountItemArmor", new[] { "AccountID" });
            DropIndex("dbo.AccountItemBoots", new[] { "ItemBootsID" });
            DropIndex("dbo.AccountItemBoots", new[] { "AccountID" });
            DropIndex("dbo.AccountItemHelmet", new[] { "ItemHelmetID" });
            DropIndex("dbo.AccountItemHelmet", new[] { "AccountID" });
            DropIndex("dbo.AccountItemLegs", new[] { "ItemLegsID" });
            DropIndex("dbo.AccountItemLegs", new[] { "AccountID" });
            DropIndex("dbo.AccountItemShield", new[] { "ItemShieldID" });
            DropIndex("dbo.AccountItemShield", new[] { "AccountID" });
            DropPrimaryKey("dbo.StatsBoost");
            AlterColumn("dbo.StatsBoost", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.StatsBoost", "Id");
            CreateIndex("dbo.StatsBoost", "Id");
            AddForeignKey("dbo.StatsBoost", "Id", "dbo.Account", "AccountID");
            DropColumn("dbo.StatsBoost", "StatsId");
            DropTable("dbo.AccountFriend");
            DropTable("dbo.AccountItemAccessory");
            DropTable("dbo.ItemAccessory");
            DropTable("dbo.AccountItemArmor");
            DropTable("dbo.ItemArmor");
            DropTable("dbo.AccountItemBoots");
            DropTable("dbo.ItemBoots");
            DropTable("dbo.AccountItemHelmet");
            DropTable("dbo.ItemHelmet");
            DropTable("dbo.AccountItemLegs");
            DropTable("dbo.ItemLegs");
            DropTable("dbo.AccountItemShield");
            DropTable("dbo.ItemShield");
            DropTable("dbo.Friend");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        FriendID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.FriendID);
            
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
                        Url = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ItemShieldID);
            
            CreateTable(
                "dbo.AccountItemShield",
                c => new
                    {
                        AccountItemShieldID = c.Int(nullable: false, identity: true),
                        ItemShieldID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemShieldID);
            
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
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ItemLegsID);
            
            CreateTable(
                "dbo.AccountItemLegs",
                c => new
                    {
                        AccountItemLegsID = c.Int(nullable: false, identity: true),
                        ItemLegsID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemLegsID);
            
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
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ItemHelmetID);
            
            CreateTable(
                "dbo.AccountItemHelmet",
                c => new
                    {
                        AccountItemHelmetID = c.Int(nullable: false, identity: true),
                        ItemHelmetID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemHelmetID);
            
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
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ItemBootsID);
            
            CreateTable(
                "dbo.AccountItemBoots",
                c => new
                    {
                        AccountItemBootsID = c.Int(nullable: false, identity: true),
                        ItemBootsID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemBootsID);
            
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
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ItemArmorID);
            
            CreateTable(
                "dbo.AccountItemArmor",
                c => new
                    {
                        AccountItemArmorID = c.Int(nullable: false, identity: true),
                        ItemArmorID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemArmorID);
            
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
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ItemAccessoryID);
            
            CreateTable(
                "dbo.AccountItemAccessory",
                c => new
                    {
                        AccountItemAccessoryID = c.Int(nullable: false, identity: true),
                        ItemAccessoryID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        Equiped = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountItemAccessoryID);
            
            CreateTable(
                "dbo.AccountFriend",
                c => new
                    {
                        AccountFriendID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        FriendID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountFriendID);
            
            AddColumn("dbo.StatsBoost", "StatsId", c => c.Int(nullable: false));
            DropForeignKey("dbo.StatsBoost", "Id", "dbo.Account");
            DropIndex("dbo.StatsBoost", new[] { "Id" });
            DropPrimaryKey("dbo.StatsBoost");
            AlterColumn("dbo.StatsBoost", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.StatsBoost", "Id");
            CreateIndex("dbo.AccountItemShield", "AccountID");
            CreateIndex("dbo.AccountItemShield", "ItemShieldID");
            CreateIndex("dbo.AccountItemLegs", "AccountID");
            CreateIndex("dbo.AccountItemLegs", "ItemLegsID");
            CreateIndex("dbo.AccountItemHelmet", "AccountID");
            CreateIndex("dbo.AccountItemHelmet", "ItemHelmetID");
            CreateIndex("dbo.AccountItemBoots", "AccountID");
            CreateIndex("dbo.AccountItemBoots", "ItemBootsID");
            CreateIndex("dbo.AccountItemArmor", "AccountID");
            CreateIndex("dbo.AccountItemArmor", "ItemArmorID");
            CreateIndex("dbo.AccountItemAccessory", "AccountID");
            CreateIndex("dbo.AccountItemAccessory", "ItemAccessoryID");
            CreateIndex("dbo.AccountFriend", "FriendID");
            CreateIndex("dbo.AccountFriend", "AccountID");
            AddForeignKey("dbo.AccountFriend", "FriendID", "dbo.Friend", "FriendID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemShield", "ItemShieldID", "dbo.ItemShield", "ItemShieldID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemShield", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemLegs", "ItemLegsID", "dbo.ItemLegs", "ItemLegsID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemLegs", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemHelmet", "ItemHelmetID", "dbo.ItemHelmet", "ItemHelmetID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemHelmet", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemBoots", "ItemBootsID", "dbo.ItemBoots", "ItemBootsID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemBoots", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemArmor", "ItemArmorID", "dbo.ItemArmor", "ItemArmorID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemArmor", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemAccessory", "ItemAccessoryID", "dbo.ItemAccessory", "ItemAccessoryID", cascadeDelete: true);
            AddForeignKey("dbo.AccountItemAccessory", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
            AddForeignKey("dbo.AccountFriend", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
        }
    }
}
