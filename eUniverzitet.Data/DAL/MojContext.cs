using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Data.DAL
{
   public class MojContext : DbContext 
    {
        public MojContext()
            : base("Name=MojConnectionString")
        {
           
        }


       protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            //one-to-(zero or one)

           // modelBuilder 
           //     .Entity<Zaposlenik>() 
           //     .Property(t => t.KorisnikId) 
           //     .HasColumnAnnotation( 
           //         "Index",  
           //         new IndexAnnotation(new[] 
           //             { 
           //                 new IndexAttribute("Index1"), 
           //                 new IndexAttribute("Index2") { IsUnique = true } 
           //             }));

           // modelBuilder
           //.Entity<Student>()
           //.Property(t => t.KorisnikId)
           //.HasColumnAnnotation(
           //    "Index",
           //    new IndexAnnotation(new[] 
           //             { 
           //                 new IndexAttribute("Index1"), 
           //                 new IndexAttribute("Index2") { IsUnique = true } 
           //             }));

            modelBuilder.Entity<Student>()
                .HasRequired(x => x.Korisnik)
                .WithMany()
                .HasForeignKey(x => x.KorisnikId);

            modelBuilder
                .Entity<Zaposlenik>()
                .HasRequired(x => x.Korisnik)
                .WithMany()
                .HasForeignKey(x => x.KorisnikId);

            //many-to-one
            //modelBuilder.Entity<Smjer>().HasRequired(x => x.Fakultet).WithMany().HasForeignKey(x=>x.FakultetId);
        
            //http://blogs.msdn.com/b/adonet/archive/2010/12/14/ef-feature-ctp5-fluent-api-samples.aspx
        }

        public DbSet<AkademskaGodina> AkademskaGodinas { set; get; }
        public DbSet<Notifikacija> Notifikacijas { set; get; }
        public DbSet<AngaziranNaPredmet> AngaziranNaPredmets { set; get; }
        public DbSet<Fakultet> Fakultets { set; get; }
        public DbSet<Rektorat> Rektorats { set; get; }
        public DbSet<IzvodjenjePredmeta> IzvodjenjePredmetas { set; get; }
        public DbSet<Korisnik> Korisniks { set; get; }
        public DbSet<NaucnaOblast> NaucnaOblasts { set; get; }
        public DbSet<NastavnoOsoblje> NastavnoOsobljes { set; get; }
        public DbSet<NPP> NPPs { set; get; }
        public DbSet<Odsjek> Odsjeks { set; get; }
        public DbSet<Opstina> Opstinas { set; get; }
        public DbSet<OrganizacionaJedinica> OrganizacionaJedinicas { set; get; }

        public DbSet<Institut> Instituts { set; get; }
        public DbSet<Predmet> Predmets { set; get; }
        public DbSet<PrijavaIspita> PrijavaIspitas { set; get; }
        public DbSet<IspitniRok> IspitniRoks { set; get; }
        public DbSet<IspitniTermin> IspitniTermins { set; get; }
     
        public DbSet<Regija> Regijas { set; get; }
        public DbSet<SlusaPredmet> SlusaPredmets { set; get; }
        public DbSet<Student> Students { set; get; }
        public DbSet<Studiranje> Studiranjes { set; get; }
        public DbSet<UpisGodine> UpisGodines { set; get; }
        public DbSet<UplataStudija> UplataStudijas { set; get; }
        public DbSet<Zaposlenik> Zaposleniks { set; get; }
        public DbSet<Zaposlenje> Zaposlenjes { set; get; }

        public DbSet<ZaposlenjeMjesto> ZaposlenjeMjestos { set; get; }


        public DbSet<Ticket> Tickets { set; get; }
        public DbSet<TicketPoruka> TicketDetails { set; get; }
        public DbSet<TicketKategorija> TicketKategorijas { set; get; }
    }
}
