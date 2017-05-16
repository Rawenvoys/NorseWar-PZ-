namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemShield", "Type", c => c.String(defaultValue: "Shield"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemShield", "Type");
        }
    }
}
