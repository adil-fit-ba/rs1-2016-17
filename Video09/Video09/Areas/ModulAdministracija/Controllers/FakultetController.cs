using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Areas.ModulAdministracija.Models;
using MVC.DAL;
using MVC.Helper;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Controllers
{
     [Autorizacija(KorisnickeUloge.Administrator)]
    public class FakultetController : Controller
    {
        private MojContext ctx = new MojContext();

        public ActionResult Index()
        {
            List<FakultetIndexVM.FakultetInfo> fakultetInfos = ctx.Fakulteti.Select(x=>new FakultetIndexVM.FakultetInfo
            {
                Id = x.Id,
                FakultetNaziv = x.Naziv,
                BrojSmjerova = ctx.Smjerovi.Where(y=>y.FakultetId == x.Id).Count(),
                BrojStudenata = ctx.Studenti.Where(y => y.Smjer.FakultetId == x.Id).Count()
            }).ToList();

            FakultetIndexVM model = new FakultetIndexVM
            {
                Fakulteti = fakultetInfos
            };
            return View(model);
        }

         public ActionResult Dodaj()
        {
            FakultetUrediVM model = new FakultetUrediVM();

            return View("Uredi", model);
        }

        public ActionResult Uredi(int fakultetId)
        {
            FakultetUrediVM model = ctx.Fakulteti
                .Where(x=>x.Id == fakultetId)
                .Select(x=>new FakultetUrediVM{
                    Id = x.Id, 
                    Opis = x.Naziv
            }).Single();

            return View(model);
        }

        public ActionResult Snimi(FakultetUrediVM input)
        {
            Fakultet fakulteteDB;
            if (input.Id == 0)
            {
                fakulteteDB = new Fakultet();
                ctx.Fakulteti.Add(fakulteteDB);
            }
            else
            {
                fakulteteDB = ctx.Fakulteti.Find(input.Id);
            }
            fakulteteDB.Naziv = input.Opis;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Obrisi(int fakultetId)
        {
            Fakultet x = ctx.Fakulteti.Find(fakultetId);
            ctx.Fakulteti.Remove(x);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}