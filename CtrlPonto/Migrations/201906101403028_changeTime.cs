namespace CtrlPonto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.trabalhos", "saldo", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.trabalhos", "saldo", c => c.Time(nullable: false));
        }
    }
}
