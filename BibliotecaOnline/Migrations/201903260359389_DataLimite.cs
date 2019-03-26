namespace BibliotecaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataLimite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmprestimoItens", "DataLimite", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmprestimoItens", "DataLimite");
        }
    }
}
