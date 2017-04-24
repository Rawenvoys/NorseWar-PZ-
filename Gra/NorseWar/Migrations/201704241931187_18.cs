namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccountQuests", "QuestActive", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountQuests", "QuestActive", c => c.Int(nullable: false));
        }
    }
}
