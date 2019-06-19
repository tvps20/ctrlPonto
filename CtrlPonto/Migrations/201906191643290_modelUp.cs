namespace CtrlPonto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelUp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.trabalhos", "horaAlmoco", c => c.Time(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.trabalhos", "horaAlmoco");
        }
    }
}
