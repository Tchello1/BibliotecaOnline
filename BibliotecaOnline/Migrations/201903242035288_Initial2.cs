namespace BibliotecaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EmprestimoItens", new[] { "Exemplares_Id" });
            DropColumn("dbo.EmprestimoItens", "ExemplarId");
            RenameColumn(table: "dbo.EmprestimoItens", name: "Exemplares_Id", newName: "ExemplarId");
            AlterColumn("dbo.EmprestimoItens", "ExemplarId", c => c.Int(nullable: false));
            CreateIndex("dbo.EmprestimoItens", "ExemplarId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.EmprestimoItens", new[] { "ExemplarId" });
            AlterColumn("dbo.EmprestimoItens", "ExemplarId", c => c.Int());
            RenameColumn(table: "dbo.EmprestimoItens", name: "ExemplarId", newName: "Exemplares_Id");
            AddColumn("dbo.EmprestimoItens", "ExemplarId", c => c.Int(nullable: false));
            CreateIndex("dbo.EmprestimoItens", "Exemplares_Id");
        }
    }
}
