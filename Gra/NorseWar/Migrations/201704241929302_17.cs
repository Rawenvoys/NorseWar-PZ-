namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountQuests", "QuestActive", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountQuests", "QuestActive");
        }
    }
}
