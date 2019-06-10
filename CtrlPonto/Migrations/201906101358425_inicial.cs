namespace CtrlPonto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.trabalhos", "jornada", c => c.Time(nullable: false));
            AlterColumn("dbo.trabalhos", "saldo", c => c.Time(nullable: false));
            AlterColumn("dbo.trabalhos", "horas", c => c.Time(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.trabalhos", "horas", c => c.DateTime());
            AlterColumn("dbo.trabalhos", "saldo", c => c.DateTime());
            AlterColumn("dbo.trabalhos", "jornada", c => c.DateTime(nullable: false));
        }
    }
}
