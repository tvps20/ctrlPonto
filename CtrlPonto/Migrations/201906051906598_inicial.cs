namespace CtrlPonto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.trabalhos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        data = c.DateTime(nullable: false),
                        Jornada = c.DateTime(nullable: false),
                        Saldo = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.pontos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        hora = c.DateTime(nullable: false),
                        tipo = c.String(nullable: false, maxLength: 30, unicode: false),
                        ativo = c.Boolean(nullable: false),
                        idTrabalho = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.trabalhos", t => t.idTrabalho)
                .Index(t => t.idTrabalho);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.pontos", new[] { "idTrabalho" });
            DropForeignKey("dbo.pontos", "idTrabalho", "dbo.trabalhos");
            DropTable("dbo.pontos");
            DropTable("dbo.trabalhos");
        }
    }
}
