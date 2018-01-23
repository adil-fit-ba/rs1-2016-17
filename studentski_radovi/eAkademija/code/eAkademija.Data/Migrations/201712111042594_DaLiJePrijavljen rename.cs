namespace eAkademija.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DaLiJePrijavljenrename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentKurs", "DaLiJePrijavljen", c => c.Boolean(nullable: false));
            DropColumn("dbo.StudentKurs", "DaLiJePriavljen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentKurs", "DaLiJePriavljen", c => c.Boolean(nullable: false));
            DropColumn("dbo.StudentKurs", "DaLiJePrijavljen");
        }
    }
}
