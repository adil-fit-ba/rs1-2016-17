using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Api.Models
{
    public class AutentifikacijaProvjeraVM
    {
        public int Korisnik { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public List<ZaposlenjaInfoVM> ZaposlenjaInfo { get; set; }
        public List<StudiranjeInfoVM> StudiranjeInfo { get; set; }

        public class ZaposlenjaInfoVM
        {
            public int OrganizacionaJedinicaId { get; set; }
            public string OrganizacionaJedinicaNaziv { get; set; }
            public KorisnickaUloga KorisnickaUloga { get; set; }
            public string ZaposlenjeMjestoNaziv { get; set; }
            public int ZaposlenjeId { get; set; }
        }

        public class StudiranjeInfoVM
        {
            public int NppId { get; set; }
            public string NppNaziv { get; set; }
            public string FakultetNaziv { get; set; }
            public string OdsjekNaziv { get; set; }
            public int PolozenoPredmeta { get; set; }
            public int StudiranjeId { get; set; }
        }
    }
}