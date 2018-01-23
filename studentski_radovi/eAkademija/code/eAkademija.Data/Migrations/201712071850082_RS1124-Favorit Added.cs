namespace eAkademija.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RS1124FavoritAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentKurs", "DaLiJeFavorit", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentKurs", "DaLiJeFavorit");
        }
    }
}
