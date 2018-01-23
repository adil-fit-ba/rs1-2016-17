using System;
using MVC.Helper;

namespace MVC.Models
{
    public class Student : IEntity
    {
        public int Id { get; set; }
   
        public string BrojIndeksa { get; set; }

        public DateTime? DatumRodjenja { get; set; }

        public bool IsDeleted { get; set; }

        public Korisnik Korisnik { get; set; }

        public  Smjer Smjer { get; set; }
        public int SmjerId { get; set; }


        public int? OpstinaRodjenjaId { get; set; }
        public Opstina OpstinaRodjenja { get; set; }

        public int? OpstinaPrebivalistaId { get; set; }
        public Opstina OpstinaPrebivalista { get; set; }


        public int? NppId { get; set; }
        public NPP Npp { get; set; }
   
    }
}
