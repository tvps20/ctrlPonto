namespace CtrlPonto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteAlter : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.pontos", "idTrabalho", "dbo.trabalhos");
            DropIndex("dbo.pontos", new[] { "idTrabalho" });
            AddForeignKey("dbo.pontos", "idTrabalho", "dbo.trabalhos", "id", cascadeDelete: true);
            CreateIndex("dbo.pontos", "idTrabalho");
        }
        
        public override void Down()
        {
            DropIndex("dbo.pontos", new[] { "idTrabalho" });
            DropForeignKey("dbo.pontos", "idTrabalho", "dbo.trabalhos");
            CreateIndex("dbo.pontos", "idTrabalho");
            AddForeignKey("dbo.pontos", "idTrabalho", "dbo.trabalhos", "id");
        }
    }
}
