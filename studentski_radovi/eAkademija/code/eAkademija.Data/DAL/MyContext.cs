using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using eAkademija.Data.Model;
using Microsoft.AspNet.Identity;
using eAkademija.Data.Enums;

namespace eAkademija.Data.DAL
{
    public class MyContext : IdentityDbContext
    {
        public  MyContext() : base("name=MyConnString"){}

        static MyContext() {
            Database.SetInitializer<MyContext>(new InitDb());
        }

        public static MyContext Create()
        {
            return new MyContext();
        }

        //MODUL: Admin
        public DbSet<AppUser> AppUserDbSet { get; set; }
        public DbSet<Grad> Gradovi { get; set; }

        //MODUL: Instruktor
        public DbSet<Kategorija> KategorijaDbSet { get; set; }
        public DbSet<Potkategorija> PotkategorijaDbSet { get; set; }

        public DbSet<Kurs> KursDbSet { get; set; }
        public DbSet<Kategorizacija> KategorizacijaDbSet { get; set; }

        public DbSet<Zadaca> ZadacaDbSet { get; set; }

        //MODUL: Student
        public DbSet<Student> StudentDbSet { get; set; }
        public DbSet<KursLajk> KursLajkDbSet { get; set; }

        //MODUL: PREKLAPAJU SE
        public DbSet<StudentKurs> StudentKursDbSet { get; set; }
        public DbSet<ZadacaStudentKurs> ZadacaStudentKursDbSet { get; set; }

        public DbSet<PitanjeOdgovor> PitanjeOdgovorDbSet { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasOptional(x => x.Student).WithRequired(x => x.AppUser);
            modelBuilder.Entity<AppUser>().HasOptional(x => x.Administrator).WithRequired(x => x.AppUser);
            modelBuilder.Entity<AppUser>().HasOptional(x => x.Instruktor).WithRequired(x => x.AppUser);

            //modelBuilder.Entity<Kategorizacija>().HasOptional(o => o.Potkategorija).WithRequired(r => r.)
        }

    }


    public class InitDb : DropCreateDatabaseIfModelChanges<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            PerformInitSetup(context);
            base.Seed(context);
        }

        public void PerformInitSetup(MyContext context)
        {

            context.Gradovi.Add(new Grad { Naziv = "Sarajevo", PostanskiBroj = "71000" });
            context.Gradovi.Add(new Grad { Naziv = "Mostar", PostanskiBroj = "88000" });
            context.Gradovi.Add(new Grad { Naziv = "Bihac", PostanskiBroj = "77000" });
            context.Gradovi.Add(new Grad { Naziv = "Zenica", PostanskiBroj = "72000" });

            context.Roles.Add(new IdentityRole("Administrator"));
            context.Roles.Add(new IdentityRole("Student"));
            context.Roles.Add(new IdentityRole("Instruktor"));

            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

            userMgr.Create(new AppUser { UserName = "root", Email = "admin@gmail.com", hasContentBan = false }, "root1234");
            AppUser admin = userMgr.FindByName("root");
            userMgr.AddToRole(admin.Id, "Administrator");

            userMgr.Create(new AppUser
            {
                UserName = "edvin",
                Email = "edvin@gmail.com",
                Ime = "Edvin",
                Prezime = "Halilovic",
                ProfilePictureName = "/profile_images/studentedvin.jpg",
                Student = new Student(),
                hasContentBan = false,
                GradId = 2
            }, "kuca1234");
            AppUser edvin = userMgr.FindByName("edvin");
            userMgr.AddToRole(edvin.Id, "Student");

            userMgr.Create(new AppUser
            {
                UserName = "damirf",
                Email = "damir.f@gmail.com",
                Ime = "Damir",
                Prezime = "Filipović",
                ProfilePictureName = "/profile_images/studentA.jpg",
                Student = new Student(),
                hasContentBan = false,
                GradId = 1
            }, "kuca1234");
            AppUser damirf = userMgr.FindByName("damirf");
            userMgr.AddToRole(damirf.Id, "Student");
            
            userMgr.Create(new AppUser
            {
                UserName = "zlatkab",
                Email = "zlatka.basic@gmail.com",
                Ime = "Zlatka",
                Prezime = "Bašić",
                ProfilePictureName = "/profile_images/studentB.jpg",
                Student = new Student(),
                hasContentBan = false,
                GradId = 3
            }, "kuca1234");
            AppUser zlatkab = userMgr.FindByName("zlatkab");
            userMgr.AddToRole(zlatkab.Id, "Student");

            userMgr.Create(new AppUser
            {
                UserName = "edinm",
                Email = "edin.ma@gmail.com",
                Ime = "Edin",
                Prezime = "Marić",
                ProfilePictureName = "/profile_images/studentC.jpg",
                Student = new Student(),
                hasContentBan = false,
                GradId = 4
            }, "kuca1234");
            AppUser edinm = userMgr.FindByName("edinm");
            userMgr.AddToRole(edinm.Id, "Student");

            // instruktor 1
            userMgr.Create(new AppUser
            {
                UserName = "denis",
                Email = "denis@gmail.com",
                Ime = "Denis",
                Prezime = "Kajdic",
                ProfilePictureName = "/profile_images/instruktor.jpg",
                Instruktor = new Instruktor(),
                hasContentBan = false,
                GradId = 2
            }, "kuca1234");
            AppUser denis = userMgr.FindByName("denis");
            userMgr.AddToRole(denis.Id, "Instruktor");

            // instruktor 2
            userMgr.Create(new AppUser
            {
                UserName = "denis2",
                Email = "denis2@gmail.com",
                Ime = "Denis2",
                Prezime = "Kajdic2",
                ProfilePictureName = "/profile_images/instruktor2.jpg",
                Instruktor = new Instruktor(),
                hasContentBan = false,
                GradId = 1
            }, "kuca1234");
            AppUser denis2 = userMgr.FindByName("denis2");
            userMgr.AddToRole(denis2.Id, "Instruktor");

            // instruktor 3
            userMgr.Create(new AppUser
            {
                UserName = "denis3",
                Email = "denis3@gmail.com",
                Ime = "Denis3",
                Prezime = "Kajdic3",
                ProfilePictureName = "/profile_images/instruktor3.jpg",
                Instruktor = new Instruktor(),
                hasContentBan = false,
                GradId = 3
            }, "kuca1234");
            AppUser denis3 = userMgr.FindByName("denis3");
            userMgr.AddToRole(denis3.Id, "Instruktor");

            
            context.SaveChanges();


            context.KategorijaDbSet.Add(new Kategorija { Naziv = "Programski jezici", KategorijaStatus = KategorijaStatus.ACTIVE });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Osnove programskog jezika C++", KategorijaId = 1 });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Osnove programskog jezika Java", KategorijaId = 1 });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Napredni C#", KategorijaId = 1 });


            context.KategorijaDbSet.Add(new Kategorija { Naziv = "Razvoj mobilnih aplikacija", KategorijaStatus = KategorijaStatus.ACTIVE });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Razvoj mobilnih Android aplikacija", KategorijaId = 2 });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Razvoj Android i iOS aplikacija u Ionic-u", KategorijaId = 2 });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Razvoj Android i iOS aplikacija u Xamarin-u", KategorijaId = 2 });


            context.KategorijaDbSet.Add(new Kategorija { Naziv = "Razvoj igara", KategorijaStatus = KategorijaStatus.ACTIVE });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Razvoj igara u Unity engine-u", KategorijaId = 3 });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Razvoj igara u Unreal engine-u", KategorijaId = 3 });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Razvoj igara za mobilne uređaje", KategorijaId = 3 });


            context.KategorijaDbSet.Add(new Kategorija { Naziv = "Web razvoj", KategorijaStatus = KategorijaStatus.ACTIVE });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Osnove HTML5 i CSS3", KategorijaId = 4 });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Razvoj ekranu prilagodljivih web aplikacija", KategorijaId = 4 });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Razvoj One-Page aplikacija u Angularu", KategorijaId = 4 });
            context.PotkategorijaDbSet.Add(new Potkategorija { Naziv = "Napredni jQuery", KategorijaId = 4 });


            context.KategorijaDbSet.Add(new Kategorija { Naziv = "Web Sigurnost", KategorijaStatus = KategorijaStatus.FLAGGED_FOR_PERMISSION });
            context.KategorijaDbSet.Add(new Kategorija { Naziv = "Javascript Design Patterns", KategorijaStatus = KategorijaStatus.FLAGGED_FOR_PERMISSION });

            context.SaveChanges();
            
            context.KursDbSet.Add(
                new Kurs
                {
                    InstruktorId = context.AppUserDbSet.Where(w => w.UserName == "denis").FirstOrDefault().Id,
                    Naziv = "C# za početnike: Naučite C# od nule",
                    DatumKreiranja = DateTime.Parse(DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0)).ToShortDateString()),
                    Opis = "Učenje C# programskog jezika od samog početka, bez potrebe da imate bilo kakvo predznanje",
                    VideoId = "gfkTfcpWqAY",
                    Nivo = Enums.KursNivo.BEGINNER,
                    Status = Enums.KursStatus.ACTIVE
                });
            context.KursDbSet.Add(
                new Kurs
                {
                    InstruktorId = context.AppUserDbSet.Where(w => w.UserName == "denis").FirstOrDefault().Id,
                    Naziv = "Kako naučiti programirati u C#-u?",
                    DatumKreiranja = DateTime.Parse(DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0)).ToShortDateString()),
                    Opis = "Kurs koji primjenjuje optimalne i dokazane metode kako bi vam približio sve prednosti C# programskog jezika -- bez muke i stresa, uz najbolji mogući efekat",
                    VideoId = "tAPo50IE0Jk",
                    Nivo = Enums.KursNivo.ADVANCED,
                    Status = Enums.KursStatus.ACTIVE
                });
            context.KursDbSet.Add(
                new Kurs
                {
                    InstruktorId = context.AppUserDbSet.Where(w => w.UserName == "denis").FirstOrDefault().Id,
                    Naziv = "C#: Izrada Windows Form liste kontakata (GUI aplikacija)",
                    DatumKreiranja = DateTime.Parse(DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0)).ToShortDateString()),
                    Opis = "Ovaj se kurs bavi kreiranjem GUI aplikacije. Cilj je da se nauče mnoge napredne metode, principi i dobre prakse u razvoju Windows desktop aplikacija",
                    VideoId = "qfH6Y7p4J7U",
                    Nivo = Enums.KursNivo.EXPERT,
                    Status = Enums.KursStatus.ACTIVE
                });
            context.KursDbSet.Add(
                new Kurs
                {
                    InstruktorId = context.AppUserDbSet.Where(w => w.UserName == "denis").FirstOrDefault().Id,
                    Naziv = "Java za početnike: Varijable",
                    DatumKreiranja = DateTime.Parse(DateTime.Now.Date.Subtract(new TimeSpan(15, 0, 0, 0)).ToShortDateString()),
                    Opis = "Varijable su neizostavni dio svakog programskog jezika, pa tako i Jave. Ovaj će kurs osigurati da naučite najbitnije koncepte u vezi s varijablama u Java programskog jeziku",
                    VideoId = "gtQJXzi3Yns",
                    Nivo = Enums.KursNivo.BEGINNER,
                    Status = Enums.KursStatus.ACTIVE
                });
            context.KursDbSet.Add(
                new Kurs
                {
                    InstruktorId = context.AppUserDbSet.Where(w => w.UserName == "denis").FirstOrDefault().Id,
                    Naziv = "Kako da sami sebe naučite programirati",
                    DatumKreiranja = DateTime.Parse(DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0)).ToShortDateString()),
                    Opis = "Ukoliko poznajete određene tajne i tehnike, možete za samo nekoliko sedmica ovladati solidnim znanjem programskog jezika C#",
                    VideoId = "lisiwUZJXqQ",
                    Nivo = Enums.KursNivo.INTERMEDIATE,
                    Status = Enums.KursStatus.ACTIVE
                });

            // instruktor 2 (denis2)
            context.KursDbSet.Add(
                new Kurs
                {
                    InstruktorId = context.AppUserDbSet.Where(w => w.UserName == "denis2").FirstOrDefault().Id,
                    Naziv = "C++ za početnike: Strukture",
                    DatumKreiranja = DateTime.Parse(DateTime.Now.Date.Subtract(new TimeSpan(16, 0, 0, 0)).ToShortDateString()),
                    Opis = "Ovaj kurs je vezan za Instruktora denis2",
                    VideoId = "eedn5isRJsc",
                    Nivo = Enums.KursNivo.BEGINNER,
                    Status = Enums.KursStatus.ACTIVE
                });


            // upisuje se u bazu kao označen za arhiviranje - za testiranje
            context.KursDbSet.Add(
                new Kurs
                {
                    InstruktorId = context.AppUserDbSet.Where(w => w.UserName == "denis").FirstOrDefault().Id,
                    Naziv = "Introduction to Kotlin",
                    DatumKreiranja = DateTime.Parse(DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0)).ToShortDateString()),
                    Opis = "Kurs koji je inicijalno flagovan za arhiviranje, prikazuje se izlistan sve dok Admin ne potvrdi arhiviranje nakon čega nestaje iz liste",
                    VideoId = "X1RVYt2QKQE",
                    Nivo = Enums.KursNivo.EXPERT,
                    Status = Enums.KursStatus.FLAGGED_FOR_ARCHIVING
                });



            context.SaveChanges();


            context.KategorizacijaDbSet.Add(new Kategorizacija { KursId = 1, PotkategorijaId = 6 });
            context.KategorizacijaDbSet.Add(new Kategorizacija { KursId = 1, PotkategorijaId = 7 });
            context.KategorizacijaDbSet.Add(new Kategorizacija { KursId = 1, PotkategorijaId = 1 });
            context.KategorizacijaDbSet.Add(new Kategorizacija { KursId = 2, PotkategorijaId = 5 });
            context.KategorizacijaDbSet.Add(new Kategorizacija { KursId = 2, PotkategorijaId = 6 });
            context.KategorizacijaDbSet.Add(new Kategorizacija { KursId = 3, PotkategorijaId = 9 });
            context.KategorizacijaDbSet.Add(new Kategorizacija { KursId = 3, PotkategorijaId = 6 });
            context.KategorizacijaDbSet.Add(new Kategorizacija { KursId = 3, PotkategorijaId = 2 });
            context.KategorizacijaDbSet.Add(new Kategorizacija { KursId = 3, PotkategorijaId = 7 });

            context.SaveChanges();


            context.ZadacaDbSet.Add(
                new Zadaca { KursId = 1, Naslov = "Zadaca prva za Kurs jedan", Opis = "Opis prve zadace za Kurs jedan", UkupnoBodova = 60, UraditiDo = DateTime.Parse(DateTime.Now.Date.Add(new TimeSpan(3, 0, 0, 0)).ToShortDateString()) }
                );
            context.ZadacaDbSet.Add(
                new Zadaca { KursId = 1, Naslov = "Zadaca druga za Kurs jedan", Opis = "Opis druge zadace za Kurs jedan", UkupnoBodova = 84, UraditiDo = DateTime.Parse(DateTime.Now.Date.Add(new TimeSpan(5, 0, 0, 0)).ToShortDateString()) }
                );
            context.ZadacaDbSet.Add(
                new Zadaca { KursId = 3, Naslov = "Zadaca prva za Kurs tri", Opis = "Opis prve zadace za Kurs tri", UkupnoBodova = 42, UraditiDo = DateTime.Parse(DateTime.Now.Date.Add(new TimeSpan(7, 0, 0, 0)).ToShortDateString()) }
                );
            context.ZadacaDbSet.Add(
                new Zadaca { KursId = 3, Naslov = "Zadaca druga za Kurs tri", Opis = "Opis druge zadace za Kurs tri", UkupnoBodova = 100, UraditiDo = DateTime.Parse(DateTime.Now.Date.Add(new TimeSpan(9, 0, 0, 0)).ToShortDateString()) }
                );


            // kurs 1
            context.StudentKursDbSet.Add(new StudentKurs { StudentId = context.AppUserDbSet.Where(x => x.UserName == "edvin").FirstOrDefault().Id, KursId = 1, Ocjena = 8 });
            context.StudentKursDbSet.Add(new StudentKurs { StudentId = context.AppUserDbSet.Where(x => x.UserName == "damirf").FirstOrDefault().Id, KursId = 1, Ocjena = -1 });
            context.StudentKursDbSet.Add(new StudentKurs { StudentId = context.AppUserDbSet.Where(x => x.UserName == "zlatkab").FirstOrDefault().Id, KursId = 1, Ocjena = -1 });
            context.StudentKursDbSet.Add(new StudentKurs { StudentId = context.AppUserDbSet.Where(w => w.UserName == "edinm").FirstOrDefault().Id, KursId = 1, Ocjena = -1 });

            // kurs 2
            context.StudentKursDbSet.Add(new StudentKurs { StudentId = context.AppUserDbSet.Where(w => w.UserName == "edinm").FirstOrDefault().Id, KursId = 2, Ocjena = -1 });
            context.StudentKursDbSet.Add(new StudentKurs { StudentId = context.AppUserDbSet.Where(w => w.UserName == "edvin").FirstOrDefault().Id, KursId = 2, Ocjena = -1 });

            // kurs 3
            context.StudentKursDbSet.Add(new StudentKurs { StudentId = context.AppUserDbSet.Where(w => w.UserName == "damirf").FirstOrDefault().Id, KursId = 3, Ocjena = -1 });
            context.StudentKursDbSet.Add(new StudentKurs { StudentId = context.AppUserDbSet.Where(w => w.UserName == "edvin").FirstOrDefault().Id, KursId = 3, Ocjena = -1 });
            context.StudentKursDbSet.Add(new StudentKurs { StudentId = context.AppUserDbSet.Where(w => w.UserName == "zlatkab").FirstOrDefault().Id, KursId = 3, Ocjena = 9 });


            context.SaveChanges();


            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 1, Poeni = 66, Rjesenje = "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?<br /><br />Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur", StudentKursId = 1 });
            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 1, Poeni = -1, Rjesenje = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.<br />Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.", StudentKursId = 2 });
            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 1, Poeni = -1, Rjesenje = "", StudentKursId = 3 });


            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 2, Poeni = -1, Rjesenje = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.", StudentKursId = 1 });
            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 2, Poeni = 72, Rjesenje = "", StudentKursId = 2 });
            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 2, Poeni = -1, Rjesenje = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.<br /><br />Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae.<br /><br />Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.", StudentKursId = 3 });



            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 3, Poeni = -1, Rjesenje = "", StudentKursId = 7 });
            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 3, Poeni = -1, Rjesenje = "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?<br /><br />Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur", StudentKursId = 8 });
            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 3, Poeni = 99, Rjesenje = "Ovo je jedno vrlo kratko ali slatko rješenje", StudentKursId = 9 });

            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 4, Poeni = 87, Rjesenje = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. ", StudentKursId = 7 });
            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 4, Poeni = -1, Rjesenje = "", StudentKursId = 8 });
            context.ZadacaStudentKursDbSet.Add(new ZadacaStudentKurs { ZadacaId = 4, Poeni = -1, Rjesenje = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.", StudentKursId = 9 });


            context.SaveChanges();


            context.KursLajkDbSet.Add(new KursLajk { DatumLajkan = DateTime.Now.Subtract(new TimeSpan(200, 0, 0, 0)), Ocjena = 4.1f, StudentKursId = 1 });

            context.KursLajkDbSet.Add(new KursLajk { DatumLajkan = DateTime.Now.Subtract(new TimeSpan(65, 0, 15, 0)), Ocjena = 3.8f, StudentKursId = 2 });

            context.KursLajkDbSet.Add(new KursLajk { DatumLajkan = DateTime.Now.Subtract(new TimeSpan(4, 8, 0, 0)), Ocjena = 4.9f,
                    StudentKursId = 3 });

            // pitanja i odgovori
            context.PitanjeOdgovorDbSet.Add(new PitanjeOdgovor { DatumPitanja = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)), Pitanje = "Dummy pitanje na kursu jedan", StudentKursId = 1, InstruktorId = context.AppUserDbSet.Where(w => w.UserName == "denis").FirstOrDefault().Id });

            context.PitanjeOdgovorDbSet.Add(new PitanjeOdgovor { DatumPitanja = DateTime.Now.Subtract(new TimeSpan(3, 5, 0, 0)), Pitanje = "Drugo dummy pitanje na Kursu 1", StudentKursId = 1, InstruktorId = context.AppUserDbSet.Where(w => w.UserName == "denis").FirstOrDefault().Id });


            context.PitanjeOdgovorDbSet.Add(new PitanjeOdgovor { DatumPitanja = DateTime.Now.Subtract(new TimeSpan(2, 15, 0, 0)), Pitanje = "Dummy prvo pitanje za kurs tri", StudentKursId = 4, InstruktorId = context.AppUserDbSet.Where(w => w.UserName == "denis").FirstOrDefault().Id });



            context.SaveChanges();
        }
    }
}
