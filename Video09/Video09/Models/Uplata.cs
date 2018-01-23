using System;
using MVC.Helper;

namespace MVC.Models
{
    public class Uplata:IEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DatumUplate { get; set; }
        public float Iznos { get; set; }
        public string Svrha { get; set; }
        public int EvidentiranoKorisnikId { get; set; }
        public Korisnik EvidentiranoKorisnik { get; set; }
        public DateTime EvidentiranoDatum { get; set; }

        public int UpisGodineId { get; set; }
        public UpisGodine UpisGodine { get; set; }
    }
}
