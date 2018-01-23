using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MVC.Models;

namespace MVC.DAL
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
            modelBuilder.Entity<Korisnik>().HasOptional(x => x.Student).WithRequired(x => x.Korisnik);
            modelBuilder.Entity<Korisnik>().HasOptional(x => x.Nastavnik).WithRequired(x => x.Korisnik);
            modelBuilder.Entity<Korisnik>().HasOptional(x => x.Referent).WithRequired(x => x.Korisnik);
            modelBuilder.Entity<Korisnik>().HasOptional(x => x.Administrator).WithRequired(x => x.Korisnik);
            
            //many-to-one
            //modelBuilder.Entity<Smjer>().HasRequired(x => x.Fakultet).WithMany().HasForeignKey(x=>x.FakultetId);
            //modelBuilder.Entity<UpisGodine>().HasRequired(x => x.Student).WithMany().HasForeignKey(x=>x.StudentId);
            //modelBuilder.Entity<UpisGodine>().HasRequired(x => x.AkademskaGodina).WithMany().HasForeignKey(x=>x.AkademskaGodinaId);
            //http://blogs.msdn.com/b/adonet/archive/2010/12/14/ef-feature-ctp5-fluent-api-samples.aspx
        }

        public DbSet<Student> Studenti { set; get; }
        public DbSet<Fakultet> Fakulteti { set; get; }
        public DbSet<Smjer> Smjerovi { set; get; }
        public DbSet<AkademskaGodina> AkademskeGodine { set; get; }
        public DbSet<UpisGodine> UpisiGodine { set; get; }
        public DbSet<Referent> Referenti { set; get; }
        public DbSet<Nastavnik> Nastavnici { set; get; }
        public DbSet<Korisnik> Korisnici { set; get; }
        public DbSet<Predaje> Predaje { set; get; }
        public DbSet<Predmet> Predmeti { set; get; }
        public DbSet<NPP> NPPs { set; get; }
        public DbSet<Regija> Regije { set; get; }
        public DbSet<Opstina> Opstine { set; get; }
        public DbSet<SlusaPredmet> SlusaPredmet { set; get; }
        public DbSet<Uplata> Uplate { set; get; }


       
      
    }
}
