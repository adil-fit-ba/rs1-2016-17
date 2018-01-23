using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Web.Mvc;
using MVC.Areas.ModulAdministracija.Models;
using MVC.DAL;
using MVC.Helper;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class PredmetController : Controller
    {
        private MojContext ctx = new MojContext();

        public ActionResult Index(int nppId)
        {
            NPP npp = ctx.NPPs.Where(x => x.Id == nppId)
             .Include(x => x.Smjer.Fakultet)
             .Include(x => x.AkademskaGodina)
             .Single();

          
            List<PredmetIndexVM.PredmetInfo> predmeti = (ctx.Predmeti
                .Where(x => x.NppId == nppId)
                .OrderBy(x=>x.GodinaStudija)
                .Select(x => new PredmetIndexVM.PredmetInfo
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    Ects = x.Ects,
                    GodinaStudija = x.GodinaStudija,
                    NppId = x.NppId
                }))
                .ToList();

            PredmetIndexVM model = new PredmetIndexVM
            {
                Predmeti = predmeti,
                FakultetNaziv = npp.Smjer.Fakultet.Naziv,
                SmjerNaziv = npp.Smjer.Naziv,
                NppNaziv = npp.AkademskaGodina.Opis + ": " + npp.Naziv,
                NppId = nppId,
            };

            return View(model);
        }

        public ActionResult Dodaj(int nppId)
        {
            NPP npp = ctx.NPPs.Where(x => x.Id == nppId)
             .Include(x => x.Smjer.Fakultet)
             .Include(x => x.AkademskaGodina)
             .Single();

            PredmetUrediVM model = new PredmetUrediVM
            {
                FakultetNaziv = npp.Smjer.Fakultet.Naziv,
                SmjerNaziv = npp.Smjer.Naziv,
                NppNaziv = npp.AkademskaGodina.Opis + ": " + npp.Naziv,
                NppId = npp.Id,
                GodinaStudijaStavke = GodinaStudijaStavke()
            };

            return View("Uredi", model);
        }

       

        public ActionResult Uredi(int predmetId)
        {
            Predmet entity = ctx.Predmeti.Where(x => x.Id == predmetId)
             .Include(x => x.Npp.Smjer.Fakultet)
             .Include(x => x.Npp.AkademskaGodina)
             .Single();

            PredmetUrediVM model = new PredmetUrediVM
            {
                Id = entity.Id,
                PredmetNaziv = entity.Naziv,
                Ects = entity.Ects,
                GodinaStudija = entity.GodinaStudija,

                FakultetNaziv = entity.Npp.Smjer.Fakultet.Naziv,
                SmjerNaziv = entity.Npp.Smjer.Naziv,
                NppNaziv = entity.Npp.AkademskaGodina.Opis + ": " + entity.Npp.Naziv,
                NppId = entity.Npp.Id,
                GodinaStudijaStavke = GodinaStudijaStavke()
            };

            return View(model);
        }

        public ActionResult Snimi(PredmetUrediVM input)
        {
            Predmet entity;
            if (input.Id == 0)
            {
                entity = new Predmet();
                ctx.Predmeti.Add(entity);
            }
            else
            {
                entity = ctx.Predmeti.Find(input.Id);
            }
            entity.Naziv = input.PredmetNaziv;
            entity.NppId = input.NppId;
            entity.GodinaStudija = input.GodinaStudija;
            entity.Ects = input.Ects;

            ctx.SaveChanges();

            return RedirectToAction("Index", new { nppId = entity.NppId});
        }

        public ActionResult Obrisi(int predmetId)
        {
            Predmet entity = ctx.Predmeti.Find(predmetId);
            ctx.Predmeti.Remove(entity);
            ctx.SaveChanges();

            return RedirectToAction("Index", new { nppId = entity.NppId });
        }



        private List<SelectListItem> GodinaStudijaStavke()
        {
            List<SelectListItem> data = new List<SelectListItem>
            {
                new SelectListItem{Value = null, Text = "(odaberite Godinu studija)"},
                new SelectListItem{Value = "1", Text = "1"},
                new SelectListItem{Value = "2", Text = "2"},
                new SelectListItem{Value = "3", Text = "3"},
            };

            return data;
        }
    }
}