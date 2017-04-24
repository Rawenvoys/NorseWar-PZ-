namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountQuests", "StartQuest", c => c.DateTime(nullable: false));
            AddColumn("dbo.AccountQuests", "EndQuesst", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountQuests", "EndQuesst");
            DropColumn("dbo.AccountQuests", "StartQuest");
        }
    }
}
