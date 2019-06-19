namespace CtrlPonto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelUp2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.trabalhos", "horasTrabalho", c => c.Time(nullable: false));
            DropColumn("dbo.trabalhos", "horas");
            DropColumn("dbo.trabalhos", "horaAlmoco");
        }
        
        public override void Down()
        {
            AddColumn("dbo.trabalhos", "horaAlmoco", c => c.Time(nullable: false));
            AddColumn("dbo.trabalhos", "horas", c => c.Time(nullable: false));
            DropColumn("dbo.trabalhos", "horasTrabalho");
        }
    }
}
