namespace BibliotecaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Exemplares2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exemplares", "CodigoDeBarras", c => c.String(maxLength: 200, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exemplares", "CodigoDeBarras", c => c.String(nullable: false, maxLength: 200, unicode: false));
        }
    }
}
