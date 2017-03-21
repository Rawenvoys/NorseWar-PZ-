namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quest", "Time", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Quest", "ExpValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quest", "ExpValue");
            DropColumn("dbo.Quest", "Time");
        }
    }
}
