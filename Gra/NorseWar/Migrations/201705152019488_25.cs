namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemAccessory", "Url", c => c.String());
            AddColumn("dbo.ItemArmor", "Url", c => c.String());
            AddColumn("dbo.ItemBoots", "Url", c => c.String());
            AddColumn("dbo.ItemHelmet", "Url", c => c.String());
            AddColumn("dbo.ItemLegs", "Url", c => c.String());
            AddColumn("dbo.ItemShield", "Url", c => c.String());
            AddColumn("dbo.ItemWeapon", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemWeapon", "Url");
            DropColumn("dbo.ItemShield", "Url");
            DropColumn("dbo.ItemLegs", "Url");
            DropColumn("dbo.ItemHelmet", "Url");
            DropColumn("dbo.ItemBoots", "Url");
            DropColumn("dbo.ItemArmor", "Url");
            DropColumn("dbo.ItemAccessory", "Url");
        }
    }
}
