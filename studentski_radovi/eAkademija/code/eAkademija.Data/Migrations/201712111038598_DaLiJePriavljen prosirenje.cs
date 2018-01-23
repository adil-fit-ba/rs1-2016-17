namespace eAkademija.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DaLiJePriavljenprosirenje : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentKurs", "DaLiJePriavljen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentKurs", "DaLiJePriavljen");
        }
    }
}
