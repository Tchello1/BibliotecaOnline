namespace BibliotecaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataLimite2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmprestimoItens", "DataLimite", c => c.DateTime());
            AlterColumn("dbo.EmprestimoItens", "DataRenovacao", c => c.DateTime());
            AlterColumn("dbo.EmprestimoItens", "ColaboradorIdRenovacao", c => c.Int());
            AlterColumn("dbo.EmprestimoItens", "DataDevolucao", c => c.DateTime());
            AlterColumn("dbo.EmprestimoItens", "ColaboradorIdDevolucao", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmprestimoItens", "ColaboradorIdDevolucao", c => c.Int(nullable: false));
            AlterColumn("dbo.EmprestimoItens", "DataDevolucao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmprestimoItens", "ColaboradorIdRenovacao", c => c.Int(nullable: false));
            AlterColumn("dbo.EmprestimoItens", "DataRenovacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmprestimoItens", "DataLimite", c => c.DateTime(nullable: false));
        }
    }
}
