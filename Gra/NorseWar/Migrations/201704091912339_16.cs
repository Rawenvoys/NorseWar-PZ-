namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guard", "Money", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guard", "Money");
        }
    }
}
