namespace BibliotecaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Exemplares : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exemplares", "Quantidade", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exemplares", "Quantidade", c => c.Int(nullable: false));
        }
    }
}
