using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    //      Autentifikacija/Provjera?username=admin&password=test
    public class AutentifikacijaController : ApiController
    {
        public class StudiranjeVM
        {
            public int NppId;
            public string NppNaziv;
            public string FakultetNaziv;
            public string OdsjekNaziv;
            public int StudiranjeId;
        }

        public class ZaposlenjaInfoVM
        {
            public int OrganizacionaJedinicaId;
            public string OrganizacionaJedinicaNaziv;
            public int KorisnickaUloga;
            public string ZaposlenjeMjestoNaziv;
        }

        public class AutentifikacijaVM 
        {
            public int KorisnikId;
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string KorisnickoIme { get; set; }
            public List<StudiranjeVM> Studiranjes { get; set; }
            public List<ZaposlenjaInfoVM> Zaposlenjas { get; set; }
        }


        private MojContext ctx = new MojContext();

        [HttpPost]
        [HttpGet]
        public AutentifikacijaVM Provjera(string username, string password)
        {
            var model = ctx.Students
                .Where(x => x.Korisnik.KorisnickoIme == username)
                .Select(a=>new AutentifikacijaVM
                {
                    KorisnikId = a.KorisnikId,
                    Ime = a.Korisnik.Ime,
                    Prezime = a.Korisnik.Prezime,
                    KorisnickoIme = a.Korisnik.KorisnickoIme,
                    Studiranjes = ctx.Studiranjes.Where(w=>w.StudentId == a.Id).Select(s=>new StudiranjeVM
                    {
                        FakultetNaziv = s.Npp.Fakultet.Naziv,
                        OdsjekNaziv= s.Npp.Odsjek.Naziv,
                        NppNaziv = s.Npp.Naziv + "(" + s.Npp.AkademskaGodina.Opis+")",
                        NppId = s.NppId,
                        StudiranjeId = s.StudentId
                    }).ToList(),
                    Zaposlenjas = ctx.Zaposlenjes.Where(w => w.ZaposlenikId == a.Id).Select(s => new ZaposlenjaInfoVM
                    {
                        OrganizacionaJedinicaId = s.OrganizacionaJedinicaId,
                        OrganizacionaJedinicaNaziv = s.OrganizacionaJedinica.Naziv,
                        ZaposlenjeMjestoNaziv = s.KorisnickaUloga + "(" + s.ZaposlenjeMjesto.Naziv + ")",
                        KorisnickaUloga = (int) s.KorisnickaUloga
                    }).ToList()
                } ).SingleOrDefault();


            return model;
        }
    }
}
