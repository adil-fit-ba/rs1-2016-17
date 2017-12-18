using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUniverzitet.Api.Models
{
    public class StudentiranjePregledVM
    {
        public class SlusaPredmeteInfoVM
        {
            public string SlusaPredmetId { get; set; }
            public string NazivPredmet { get; set; }
            public int? Ocjena { get; set; }
            public DateTime? Datum { get; set; }
            public bool Priznato { get; set; }
            public float Ects { get; set; }
            public int GodinaStudija { get; set; }
        }
        public class UplataStudijaVM
        {
            public int UplataId { get; set; }
            public DateTime DatumUplate { get; set; }
            public string Svrha { get; set; }
            public float Iznos { get; set; }
            public string EvidentiranoKorisnik { get; set; }
            public DateTime EvidentiranoDatum { get; set; }
        }

        public class UpisGodineStudijaVM
        {
            public int UpisGodineId { get; set; }
            public int GodinaStudija { get; set; }
            public string AkademskaGodina { get; set; }
            public List<UplataStudijaVM> UplataStudijaInfo { get; set; }
            public float Cijena { get; set; }
            public float Uplaceno { get; set; }
        }

        public class PrijavljeniIspitiVM
        {
            public int PrijavaIspitaId { get; set; }
            public int GodinaStudija { get; set; }
            public string Predmet { get; set; }
            public string Nastavnik { get; set; }
            public DateTime DatumIspita { get; set; }
            public DateTime DatumPrijava { get; set; }
            public DateTime? DatumOdjava { get; set; }
        }

        public string BrojIndeksa { get; set; }
        public String Fakultet { get; set; }
        public String Odsjek { get; set; }
        public String Npp { get; set; }
        public int? TrenutnaGodinaStudija { get; set; }

        public List<UpisGodineStudijaVM> UpisGodineStudijaInfo{ get; set; }
        public List<SlusaPredmeteInfoVM> SlusaPredmeteInfo{ get; set; }
        public List<PrijavljeniIspitiVM> PrijavljeniIspitiInfo { get; set; }
    }
}