namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccountQuests", "Quest1", c => c.Int());
            AlterColumn("dbo.AccountQuests", "Quest2", c => c.Int());
            AlterColumn("dbo.AccountQuests", "Quest3", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountQuests", "Quest3", c => c.Int(nullable: false));
            AlterColumn("dbo.AccountQuests", "Quest2", c => c.Int(nullable: false));
            AlterColumn("dbo.AccountQuests", "Quest1", c => c.Int(nullable: false));
        }
    }
}
