namespace BibliotecaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LivrosExemplares : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exemplares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoDeBarras = c.String(maxLength: 200, unicode: false),
                        Estante = c.String(maxLength: 200, unicode: false),
                        Setor = c.String(maxLength: 200, unicode: false),
                        Campos = c.String(maxLength: 200, unicode: false),
                        Status = c.Int(nullable: false),
                        LivroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Livros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Autor = c.String(maxLength: 200, unicode: false),
                        Editora = c.String(maxLength: 200, unicode: false),
                        Ano = c.Int(nullable: false),
                        Idioma = c.String(),
                        ISBN = c.String(maxLength: 200, unicode: false),
                        Edicao = c.String(maxLength: 200, unicode: false),
                        LivroExemplarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exemplares", t => t.LivroExemplarId, cascadeDelete: true)
                .Index(t => t.LivroExemplarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Livros", "LivroExemplarId", "dbo.Exemplares");
            DropIndex("dbo.Livros", new[] { "LivroExemplarId" });
            DropTable("dbo.Livros");
            DropTable("dbo.Exemplares");
        }
    }
}
