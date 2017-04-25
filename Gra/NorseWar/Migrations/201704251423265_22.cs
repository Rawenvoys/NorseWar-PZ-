namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quest", "TimeSecond", c => c.Int(nullable: false));
            DropColumn("dbo.Quest", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quest", "Time", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Quest", "TimeSecond");
        }
    }
}
