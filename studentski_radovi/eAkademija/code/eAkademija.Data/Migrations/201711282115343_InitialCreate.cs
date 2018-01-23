namespace eAkademija.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Ime = c.String(),
                        Prezime = c.String(),
                        hasContentBan = c.Boolean(),
                        ProfilePictureName = c.String(),
                        GradId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grads", t => t.GradId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.GradId);
            
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.KursArhivas",
                c => new
                    {
                        AdministratorId = c.String(nullable: false, maxLength: 128),
                        KursId = c.Int(nullable: false),
                        Naziv = c.String(),
                        DatumArhiviran = c.DateTime(nullable: false),
                        Razlog = c.String(),
                    })
                .PrimaryKey(t => new { t.AdministratorId, t.KursId })
                .ForeignKey("dbo.Administrators", t => t.AdministratorId, cascadeDelete: true)
                .ForeignKey("dbo.Kurs", t => t.KursId, cascadeDelete: true)
                .Index(t => t.AdministratorId)
                .Index(t => t.KursId);
            
            CreateTable(
                "dbo.Kurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstruktorId = c.String(maxLength: 128),
                        DatumKreiranja = c.DateTime(nullable: false),
                        Naziv = c.String(),
                        Opis = c.String(),
                        VideoId = c.String(),
                        Nivo = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instruktors", t => t.InstruktorId)
                .Index(t => t.InstruktorId);
            
            CreateTable(
                "dbo.Instruktors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Grads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        PostanskiBroj = c.String(),
                        DrzavaId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drzavas", t => t.DrzavaId)
                .Index(t => t.DrzavaId);
            
            CreateTable(
                "dbo.Drzavas",
                c => new
                    {
                        DrzavaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.DrzavaId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.StudentKurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 128),
                        KursId = c.Int(),
                        DatumPocetak = c.DateTime(),
                        DatumKraj = c.DateTime(),
                        Ocjena = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kurs", t => t.KursId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.KursId);
            
            CreateTable(
                "dbo.StudentZnackas",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        ZnackaId = c.Int(nullable: false),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.ZnackaId })
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Znackas", t => t.ZnackaId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.ZnackaId);
            
            CreateTable(
                "dbo.Znackas",
                c => new
                    {
                        ZnackaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ZnackaId);
            
            CreateTable(
                "dbo.Kategorijas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        KategorijaStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Potkategorijas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        KategorijaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategorijas", t => t.KategorijaId, cascadeDelete: true)
                .Index(t => t.KategorijaId);
            
            CreateTable(
                "dbo.Kategorizacijas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KursId = c.Int(nullable: false),
                        PotkategorijaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kurs", t => t.KursId, cascadeDelete: true)
                .ForeignKey("dbo.Potkategorijas", t => t.PotkategorijaId, cascadeDelete: true)
                .Index(t => t.KursId)
                .Index(t => t.PotkategorijaId);
            
            CreateTable(
                "dbo.KursLajks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatumLajkan = c.DateTime(nullable: false),
                        Ocjena = c.Single(nullable: false),
                        StudentKursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentKurs", t => t.StudentKursId, cascadeDelete: true)
                .Index(t => t.StudentKursId);
            
            CreateTable(
                "dbo.PitanjeOdgovors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatumPitanja = c.DateTime(nullable: false),
                        DatumOdgovora = c.DateTime(),
                        Pitanje = c.String(),
                        Odgovor = c.String(),
                        StudentKursId = c.Int(nullable: false),
                        InstruktorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instruktors", t => t.InstruktorId)
                .ForeignKey("dbo.StudentKurs", t => t.StudentKursId, cascadeDelete: true)
                .Index(t => t.StudentKursId)
                .Index(t => t.InstruktorId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Zadacas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naslov = c.String(),
                        Opis = c.String(),
                        UkupnoBodova = c.Int(nullable: false),
                        UraditiDo = c.DateTime(nullable: false),
                        KursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kurs", t => t.KursId, cascadeDelete: true)
                .Index(t => t.KursId);
            
            CreateTable(
                "dbo.ZadacaStudentKurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatumNapisan = c.DateTime(),
                        Rjesenje = c.String(),
                        Poeni = c.Int(nullable: false),
                        ZadacaId = c.Int(nullable: false),
                        StudentKursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentKurs", t => t.StudentKursId, cascadeDelete: true)
                .ForeignKey("dbo.Zadacas", t => t.ZadacaId, cascadeDelete: true)
                .Index(t => t.ZadacaId)
                .Index(t => t.StudentKursId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZadacaStudentKurs", "ZadacaId", "dbo.Zadacas");
            DropForeignKey("dbo.ZadacaStudentKurs", "StudentKursId", "dbo.StudentKurs");
            DropForeignKey("dbo.Zadacas", "KursId", "dbo.Kurs");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PitanjeOdgovors", "StudentKursId", "dbo.StudentKurs");
            DropForeignKey("dbo.PitanjeOdgovors", "InstruktorId", "dbo.Instruktors");
            DropForeignKey("dbo.KursLajks", "StudentKursId", "dbo.StudentKurs");
            DropForeignKey("dbo.Kategorizacijas", "PotkategorijaId", "dbo.Potkategorijas");
            DropForeignKey("dbo.Kategorizacijas", "KursId", "dbo.Kurs");
            DropForeignKey("dbo.Potkategorijas", "KategorijaId", "dbo.Kategorijas");
            DropForeignKey("dbo.Students", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentZnackas", "ZnackaId", "dbo.Znackas");
            DropForeignKey("dbo.StudentZnackas", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentKurs", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentKurs", "KursId", "dbo.Kurs");
            DropForeignKey("dbo.Instruktors", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Grads", "DrzavaId", "dbo.Drzavas");
            DropForeignKey("dbo.AspNetUsers", "GradId", "dbo.Grads");
            DropForeignKey("dbo.Administrators", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.KursArhivas", "KursId", "dbo.Kurs");
            DropForeignKey("dbo.Kurs", "InstruktorId", "dbo.Instruktors");
            DropForeignKey("dbo.KursArhivas", "AdministratorId", "dbo.Administrators");
            DropIndex("dbo.ZadacaStudentKurs", new[] { "StudentKursId" });
            DropIndex("dbo.ZadacaStudentKurs", new[] { "ZadacaId" });
            DropIndex("dbo.Zadacas", new[] { "KursId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PitanjeOdgovors", new[] { "InstruktorId" });
            DropIndex("dbo.PitanjeOdgovors", new[] { "StudentKursId" });
            DropIndex("dbo.KursLajks", new[] { "StudentKursId" });
            DropIndex("dbo.Kategorizacijas", new[] { "PotkategorijaId" });
            DropIndex("dbo.Kategorizacijas", new[] { "KursId" });
            DropIndex("dbo.Potkategorijas", new[] { "KategorijaId" });
            DropIndex("dbo.StudentZnackas", new[] { "ZnackaId" });
            DropIndex("dbo.StudentZnackas", new[] { "StudentId" });
            DropIndex("dbo.StudentKurs", new[] { "KursId" });
            DropIndex("dbo.StudentKurs", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Grads", new[] { "DrzavaId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Instruktors", new[] { "Id" });
            DropIndex("dbo.Kurs", new[] { "InstruktorId" });
            DropIndex("dbo.KursArhivas", new[] { "KursId" });
            DropIndex("dbo.KursArhivas", new[] { "AdministratorId" });
            DropIndex("dbo.Administrators", new[] { "Id" });
            DropIndex("dbo.AspNetUsers", new[] { "GradId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.ZadacaStudentKurs");
            DropTable("dbo.Zadacas");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PitanjeOdgovors");
            DropTable("dbo.KursLajks");
            DropTable("dbo.Kategorizacijas");
            DropTable("dbo.Potkategorijas");
            DropTable("dbo.Kategorijas");
            DropTable("dbo.Znackas");
            DropTable("dbo.StudentZnackas");
            DropTable("dbo.StudentKurs");
            DropTable("dbo.Students");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Drzavas");
            DropTable("dbo.Grads");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Instruktors");
            DropTable("dbo.Kurs");
            DropTable("dbo.KursArhivas");
            DropTable("dbo.Administrators");
            DropTable("dbo.AspNetUsers");
        }
    }
}
