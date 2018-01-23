using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MVC.Areas.ModulAdministracija.Models;
using MVC.DAL;
using MVC.Helper;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class PredajeController : Controller
    {
        private MojContext ctx = new MojContext();

        public ActionResult Index(int predmetId)
        {
            Predmet predmet = ctx.Predmeti.Where(x => x.Id == predmetId)
                .Include(x => x.Npp)
                .Include(x => x.Npp.Smjer)
                .Include(x => x.Npp.Smjer.Fakultet)
            .Single();

            List<PredajeIndexVM.PredajeInfo> predajeList = ctx.Predaje
                .Where(x => x.PredmetId == predmetId)
                .Select(x => new PredajeIndexVM.PredajeInfo
                {
                    Id = x.Id,
                    AkademskaGodina = x.AkademskaGodina.Opis,
                    Nastavnik =
                        x.Nastavnik.titula + " " + x.Nastavnik.Korisnik.Ime + " " + x.Nastavnik.Korisnik.Prezime,
                }).ToList();
           
            PredajeIndexVM model = new PredajeIndexVM
            {
                PredajeList =  predajeList,

                FakultetNaziv = predmet.Npp.Smjer.Fakultet.Naziv,
                SmjerNaziv = predmet.Npp.Smjer.Naziv,
                NppNaziv = predmet.Npp.Naziv,
                PredmetNaziv = predmet.Naziv,
                PredmetId = predmet.Id,
            };



            return View("Index", model);
        }

        public ActionResult Obrisi(int predajeId)
        {
            Predaje x = ctx.Predaje.Find(predajeId);
            ctx.Predaje.Remove(x);
            ctx.SaveChanges();

            return RedirectToAction("Index", new { predmetId = x.PredmetId });
        }

        public ActionResult Dodaj(int predmetId)
        {
            Predmet predmet = ctx.Predmeti
                .Where(x => x.Id == predmetId)
                .Include(x => x.Npp.Smjer.Fakultet)
            .Single();


            PredajeUrediVM model = new PredajeUrediVM
            {
                NastavniciStavke = NastavniciStavke(),
                AkademskeGodineStavke = AkademskeGodineStavke(),
              
                PredmetId = predmet.Id,
                PredmetNaziv = predmet.Naziv,
                SmjerNaziv = predmet.Npp.Smjer.Naziv,
                FakultetNaziv = predmet.Npp.Smjer.Fakultet.Naziv,
                NppNaziv = predmet.Npp.Naziv
            };

            return View("Uredi", model);
        }

        public ActionResult Uredi(int predajeId)
        {
            Predaje predaje = ctx.Predaje
                    .Where(x=>x.Id == predajeId)
                    .Include(x=>x.Predmet.Npp.Smjer.Fakultet)
                    .Single();

            Predmet predmet = predaje.Predmet;

            PredajeUrediVM Model = new PredajeUrediVM
            {
                Id = predaje.Id,
              
                NastavnikId = predaje.NastavnikId,
                NastavniciStavke = NastavniciStavke(),
                AkademskaGodinaId = predaje.AkademskaGodinaId,
                AkademskeGodineStavke = AkademskeGodineStavke(),

                PredmetId = predmet.Id,
                PredmetNaziv = predmet.Naziv,
                SmjerNaziv = predmet.Npp.Smjer.Naziv,
                FakultetNaziv = predmet.Npp.Smjer.Fakultet.Naziv,
                NppNaziv = predmet.Npp.Naziv
            };

            return View(Model);
        }

        public ActionResult Snimi(Predaje input)
        {
            Predaje predajeDB;
            if (input.Id == 0)
            {
                predajeDB = new Predaje();
                ctx.Predaje.Add(predajeDB);
            }
            else
            {
                predajeDB = ctx.Predaje.Find(input.Id);
            }
            predajeDB.NastavnikId = input.NastavnikId;
            predajeDB.PredmetId = input.PredmetId;
            predajeDB.AkademskaGodinaId = input.AkademskaGodinaId;
            ctx.SaveChanges();

            return RedirectToAction("Index", new { predmetId = input.PredmetId });
        }

        private List<SelectListItem> NastavniciStavke()
        {
            return ctx.Nastavnici
                .Include(x=>x.Korisnik)
                .AsEnumerable()
                .Select(x => new SelectListItem{
                    Value = x.Id.ToString(),
                    Text = x.Korisnik.FullName
                })
                .ToList();
        }

        private List<SelectListItem> AkademskeGodineStavke()
        {
            return ctx.AkademskeGodine
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Opis
                })
                .ToList();
        }
    }
}