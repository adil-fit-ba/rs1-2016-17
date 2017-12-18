using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eUniverzitet.Api.Models;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Api.Controllers
{
    public class StudiranjeController : ApiController
    {
        private MojContext ctx = new MojContext();

        [HttpGet]
        public StudentiranjePregledVM Pregled(int studiranjeId)
        {
          
            StudentiranjePregledVM model = ctx.Studiranjes.Where(x=>x.Id == studiranjeId).Select(s=>new StudentiranjePregledVM
            {
                Fakultet = s.Npp.Fakultet.Naziv,
                Odsjek = s.Npp.Odsjek.Naziv,
                Npp = s.Npp.Naziv,
                BrojIndeksa = s.BrojIndeksa,

                SlusaPredmeteInfo = ctx.SlusaPredmets.Where(x => x.UpisGodine.StudiranjeId == studiranjeId)
                    .Select(x => new StudentiranjePregledVM.SlusaPredmeteInfoVM
                    {
                        GodinaStudija = x.UpisGodine.GodinaStudija,
                        NazivPredmet = x.IzvodjenjePredmeta.Predmet.Naziv,
                        Ects = x.IzvodjenjePredmeta.Predmet.Ects,
                        Ocjena = x.FinalnaOcjena,
                        Datum = x.DatumOcjene,
                        Priznato = x.Priznato
                    }).ToList(),

                UpisGodineStudijaInfo = ctx.UpisGodines.Where(u => u.StudiranjeId == studiranjeId).Select(p => new StudentiranjePregledVM.UpisGodineStudijaVM()
                    {
                       AkademskaGodina = p.AkademskaGodina.Opis,
                       GodinaStudija = p.GodinaStudija,
                       Cijena = p.Cijena,
                       Uplaceno = ctx.UplataStudijas.Where(u => u.UpisGodineId == p.Id).Sum(u=>(float?)u.Iznos)??0,
                       UplataStudijaInfo = ctx.UplataStudijas.Where(u=>u.UpisGodineId==p.Id).Select(u=>new StudentiranjePregledVM.UplataStudijaVM
                       {
                        DatumUplate   = u.DatumUplate,
                        EvidentiranoDatum = u.EvidentiranoDatum,
                        EvidentiranoKorisnik = u.EvidentiranoKorisnik.Ime + " " + u.EvidentiranoKorisnik.Prezime,
                        Iznos = u.Iznos,
                        Svrha = u.Svrha,
                        UplataId = u.Id
                       }).ToList()
                       
                    }).ToList(),

                    PrijavljeniIspitiInfo = ctx.PrijavaIspitas.Where(p=>p.SlusaPredmet.UpisGodine.StudiranjeId == studiranjeId).Select(p=>new StudentiranjePregledVM.PrijavljeniIspitiVM
                    {
                        PrijavaIspitaId = p.Id,
                        GodinaStudija =p.SlusaPredmet.IzvodjenjePredmeta.Predmet.GodinaStudija,
                        Predmet =p.SlusaPredmet.IzvodjenjePredmeta.Predmet.Naziv,
                        Nastavnik = 
                            ctx.AngaziranNaPredmets
                                .Where(a=>a.IzvodjenjePredmetaId == p.SlusaPredmet.IzvodjenjePredmetaId)
                                .FirstOrDefault(a=>a.AngaziranNaPredmetTip == AngaziranNaPredmetTip.PredmetniNastavnik)
                                .NastavnoOsoblje.Zaposlenik.Korisnik.Ime + " " + 

                            ctx.AngaziranNaPredmets
                                .Where(a => a.IzvodjenjePredmetaId == p.SlusaPredmet.IzvodjenjePredmetaId)
                                .FirstOrDefault(a => a.AngaziranNaPredmetTip == AngaziranNaPredmetTip.PredmetniNastavnik)
                                .NastavnoOsoblje.Zaposlenik.Korisnik.Prezime,
                        DatumPrijava = p.PrijavaDatum,
                        DatumOdjava = p.OdjavaDatum,
                        DatumIspita = p.IspitniTermin.VrijemeIspita,

                    }).ToList()

            }).Single();


            UpisGodine zadnjiUpisGodine = ctx.UpisGodines
                .Where(x => x.StudiranjeId == studiranjeId)
                .OrderByDescending(x => x.GodinaStudija)
                .FirstOrDefault();

            if (zadnjiUpisGodine != null)
            {
                model.TrenutnaGodinaStudija = zadnjiUpisGodine.GodinaStudija;
                
            }

            return model;
        }
    }
}
