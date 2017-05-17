namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemShield", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemShield", "Type");
        }
    }
}
