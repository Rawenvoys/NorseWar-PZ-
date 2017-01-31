namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Account", "CharacterClass", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Account", "CharacterClass", c => c.String(nullable: false));
        }
    }
}
