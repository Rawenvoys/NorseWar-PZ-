namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Account", "BanTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Account", "BanTime", c => c.Int());
        }
    }
}



