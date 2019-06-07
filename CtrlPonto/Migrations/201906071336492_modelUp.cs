namespace CtrlPonto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelUp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.pontos", "tipo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.pontos", "tipo", c => c.String(nullable: false, maxLength: 30, unicode: false));
        }
    }
}
