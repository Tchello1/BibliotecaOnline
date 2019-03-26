namespace BibliotecaOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exemplares", "campus", c => c.String(nullable: false, maxLength: 200, unicode: false));
            DropColumn("dbo.Exemplares", "Campos");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exemplares", "Campos", c => c.String(nullable: false, maxLength: 200, unicode: false));
            DropColumn("dbo.Exemplares", "campus");
        }
    }
}
