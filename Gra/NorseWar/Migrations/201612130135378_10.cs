namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "BanTime", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "BanTime");
        }
    }
}
