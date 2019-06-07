namespace CtrlPonto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chageTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.pontos", "hora", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.pontos", "hora", c => c.Time(nullable: false));
        }
    }
}
