namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Account", "AccountMessageID");
            DropColumn("dbo.Message", "AccountMessageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Message", "AccountMessageID", c => c.Int(nullable: false));
            AddColumn("dbo.Account", "AccountMessageID", c => c.Int(nullable: false));
        }
    }
}
