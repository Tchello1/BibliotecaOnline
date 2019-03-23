namespace BibliotecaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidades",
                c => new
                    {
                        Codigo = c.String(nullable: false, maxLength: 200, unicode: false),
                        Nome = c.String(maxLength: 200, unicode: false),
                        UF = c.String(maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.Estados",
                c => new
                    {
                        CodigoUf = c.String(nullable: false, maxLength: 200, unicode: false),
                        Nome = c.String(maxLength: 200, unicode: false),
                        Uf = c.String(maxLength: 2, unicode: false),
                        Regiao = c.String(),
                    })
                .PrimaryKey(t => t.CodigoUf);
            
            CreateTable(
                "dbo.Exemplares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoDeBarras = c.String(nullable: false, maxLength: 200, unicode: false),
                        Estante = c.String(maxLength: 200, unicode: false),
                        Setor = c.String(maxLength: 200, unicode: false),
                        Campos = c.String(nullable: false, maxLength: 200, unicode: false),
                        Quantidade = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        LivroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Livros", t => t.LivroId)
                .Index(t => t.LivroId);
            
            CreateTable(
                "dbo.Livros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(maxLength: 200, unicode: false),
                        Autor = c.String(maxLength: 200, unicode: false),
                        Editora = c.String(maxLength: 200, unicode: false),
                        Ano = c.String(maxLength: 8000, unicode: false),
                        Idioma = c.String(maxLength: 8000, unicode: false),
                        ISBN = c.String(maxLength: 200, unicode: false),
                        Edicao = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipo = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 200, unicode: false),
                        Email = c.String(nullable: false, maxLength: 200, unicode: false),
                        Matricula = c.String(nullable: false, maxLength: 200, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 200, unicode: false),
                        Cidade = c.String(nullable: false, maxLength: 200, unicode: false),
                        UF = c.String(nullable: false, maxLength: 2, unicode: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exemplares", "LivroId", "dbo.Livros");
            DropIndex("dbo.Exemplares", new[] { "LivroId" });
            DropTable("dbo.Pessoas");
            DropTable("dbo.Livros");
            DropTable("dbo.Exemplares");
            DropTable("dbo.Estados");
            DropTable("dbo.Cidades");
        }
    }
}
