namespace BibliotecaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pessoa : DbMigration
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
            DropTable("dbo.Pessoas");
            DropTable("dbo.Estados");
            DropTable("dbo.Cidades");
        }
    }
}
