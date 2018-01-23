using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using MVC.Areas.ModulStudentskaSluzba.Models;
using MVC.Areas.ModulStudentskaSluzba.Models.TokStudija;
using MVC.DAL;
using MVC.Models;

namespace MVC.Areas.ModulStudentskaSluzba.Controllers
{
    public class TokStudijaController : Controller
    {
        MojContext ctx = new MojContext();

        public ActionResult SnimiUpisGodine(UpisGodineDetaljnoVM input)
        {
            UpisGodine x = ctx.UpisiGodine.Single(z => z.Id == input.Id);
            x.Datum1_ZimskiUpis = input.Datum1_ZimskiUpis;
            x.Datum2_ZimskiOvjera = input.Datum2_ZimskiOvjera;
            x.Datum3_LjetniUpis = input.Datum3_LjetniUpis;
            x.Datum4_LjetniOvjera = input.Datum4_LjetniOvjera;
            x.GodinaStudija = input.GodinaStudija;
            x.AkademskaGodinaId = input.AkademskaGodinaId;
            x.Cijena = input.Cijena;

            ctx.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Index(int studentId, int? upisGodineId, int? uplataId, int? slusaPredmetId)
        {
            TokStudijaIndexVM model = new TokStudijaIndexVM();

            model.OpsteInfo = ctx.Studenti.Where(x => x.Id == studentId)
                .Select(x => new OpsteInfoVM
                {
                    StudentId =  studentId,
                    Ime = x.Korisnik.Ime,
                    OpstinaPrebivalista = x.OpstinaPrebivalista.Opis,
                    OpstinaRodjenja = x.OpstinaRodjenja.Opis,
                    Prezime = x.Korisnik.Prezime,
                    BrojIndeksa = x.BrojIndeksa,
                    NPP = x.Npp.Naziv,
                    Smjer = x.Smjer.Naziv,
                    Fakultet = x.Smjer.Fakultet.Naziv
                }).Single();


            model.UpisaneGodine = ctx.UpisiGodine
                .Where(x => x.StudentId == studentId)
                .Select(x => new UpisaneGodineVM
                {
                    Id = x.Id,
                    StudentId = x.StudentId,
                    GodinaStudija = x.GodinaStudija,
                    Cijena = x.Cijena,
                    PolozenoPredmeta =
                        ctx.SlusaPredmet.Where(y => y.UpisGodineId == x.Id).Count(z => z.FinalnaOcjena > 5),
                    Datum1_ZimskiUpis = x.Datum1_ZimskiUpis,
                    Datum2_ZimskiOvjera = x.Datum2_ZimskiOvjera,
                    Datum3_LjetniUpis = x.Datum3_LjetniUpis,
                    Datum4_LjetniOvjera = x.Datum4_LjetniOvjera
                })
                .ToList();

            model.SlusaPredmete = ctx.SlusaPredmet
                .Where(x => x.UpisGodine.StudentId == studentId)
                .Select(x => new SlusaPredmeteVM
                {
                    DatumOcjene = x.DatumOcjene,
                    ECTS = x.Predaje.Predmet.Ects,
                    GodinaStudija = x.UpisGodine.GodinaStudija,
                    NazivPredmeta = x.Predaje.Predmet.Naziv,
                    Ocjena = x.FinalnaOcjena
                }).ToList();

            model.Uplate = ctx.Uplate
                    .Where(x => x.UpisGodine.StudentId == studentId)
                    .Select(x => new UplataVM
                    {
                        DatumUplate = x.DatumUplate,
                        EvidentiranoDatum = x.EvidentiranoDatum,
                        EvidentiranoKorisnik = x.EvidentiranoKorisnik.Ime + " " + x.EvidentiranoKorisnik.Prezime,
                        Iznos = x.Iznos,
                        Svrha = x.Svrha
                    })
                    .ToList();


            if (upisGodineId.HasValue)
                model.UpisGodineDetaljno = ctx.UpisiGodine.Where(z => z.Id == upisGodineId)
                    .Select(x => new UpisGodineDetaljnoVM
                    {
                        Id = x.Id,
                        AkademskaGodinaId = x.AkademskaGodinaId,
                        GodinaStudija = x.GodinaStudija,
                        Datum1_ZimskiUpis = x.Datum1_ZimskiUpis,
                        Datum2_ZimskiOvjera = x.Datum2_ZimskiOvjera,
                        Datum3_LjetniUpis = x.Datum3_LjetniUpis,
                        Datum4_LjetniOvjera = x.Datum4_LjetniOvjera,
                        AkademskaGodinaOptions = ctx.AkademskeGodine.Select(y => new SelectListItem { Value = y.Id.ToString(), Text = y.Opis })
                    }).Single();

            return View(model);
        }

       

    }
}