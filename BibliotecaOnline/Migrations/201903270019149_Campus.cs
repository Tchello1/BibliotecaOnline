namespace BibliotecaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exemplares", "Campus", c => c.String(nullable: false, maxLength: 200, unicode: false));
            DropColumn("dbo.Exemplares", "Campos");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exemplares", "Campos", c => c.String(nullable: false, maxLength: 200, unicode: false));
            DropColumn("dbo.Exemplares", "Campus");
        }
    }
}
