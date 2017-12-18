using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Api.Controllers
{

    public class PrijavaIspitaController : ApiController
    {
        public class IspitiVM
        {
            public String predmet { get; set; }
            public String nastavnik { get; set; }
            public DateTime ispitDatum { get; set; }
            public String ucionica { get; set; }
            public DateTime? prijavaDatum { get; set; }
            public DateTime? odjavaDatum { get; set; }
        }

        public class VM
        {
            public List<IspitiVM> IspitLista { get; set; }

        }

        private MojContext ctx = new MojContext();

        [HttpGet]
        public VM GetAll(int studiranjeId)
        {
            return new VM
            {
                IspitLista = ctx.IspitniTermins
                   // .Where(x=>ctx.IzvodjenjePredmetas.Any(a=>a.UpisGodineId == studiranjeId))
                    .Select(s => new IspitiVM
                    {
                        ispitDatum = s.VrijemeIspita,
                        nastavnik = ctx.AngaziranNaPredmets.Where(a=>a.IzvodjenjePredmetaId == s.IzvodjenjePredmeta.Id && a.AngaziranNaPredmetTip == AngaziranNaPredmetTip.PredmetniNastavnik).Select(a=>a.NastavnoOsoblje.Zaposlenik.Korisnik.Ime).FirstOrDefault(),
                        predmet = s.IzvodjenjePredmeta.Predmet.Naziv,
                        ucionica = s.MjestoIspita,
                        prijavaDatum = ctx.PrijavaIspitas.Where(p=>p.IspitniTerminId == s.Id  /* && p.SlusaPredmet.UpisGodine.StudiranjeId == studiranjeId*/).Select(p=>p.PrijavaDatum).FirstOrDefault(),
                        odjavaDatum = ctx.PrijavaIspitas.Where(p => p.IspitniTerminId == s.Id /* && p.SlusaPredmet.UpisGodine.StudiranjeId == studiranjeId*/).Select(p => p.OdjavaDatum).FirstOrDefault()
                    }).ToList()
            };
        }
    }
}
