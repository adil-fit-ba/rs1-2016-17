using System;
using MVC.Helper;

namespace MVC.Models
{
    public class Korisnik:IEntity
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

     
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public Student Student { get; set; }
        public Nastavnik Nastavnik { get; set; }
        public Referent Referent { get; set; }
        public Administrator Administrator{ get; set; }
        public string FullName { get { return Ime + " " + Prezime; } }

    }
}
