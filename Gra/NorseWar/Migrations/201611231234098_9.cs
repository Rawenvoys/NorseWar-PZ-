namespace NorseWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Account", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Account", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Account", "Mail", c => c.String(nullable: false));
            AlterColumn("dbo.Account", "CharacterClass", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Account", "CharacterClass", c => c.String());
            AlterColumn("dbo.Account", "Mail", c => c.String());
            AlterColumn("dbo.Account", "Password", c => c.String());
            AlterColumn("dbo.Account", "Login", c => c.String());
        }
    }
}
