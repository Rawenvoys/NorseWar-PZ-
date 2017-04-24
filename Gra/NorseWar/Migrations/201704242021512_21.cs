namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccountQuests", "StartQuest", c => c.DateTime());
            AlterColumn("dbo.AccountQuests", "EndQuesst", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountQuests", "EndQuesst", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AccountQuests", "StartQuest", c => c.DateTime(nullable: false));
        }
    }
}
