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
    public class SmjerController : Controller
    {
        private MojContext ctx = new MojContext();

        public ActionResult Index(int fakultetId)
        {
            Fakultet fakultet = ctx.Fakulteti.Find(fakultetId);

            List<SmjerIndexVM.SmjerInfo> smjerovi = (ctx.Smjerovi
                .Where(x => x.FakultetId == fakultetId)
                .Include(x => x.Fakultet)
                .Select(x => new SmjerIndexVM.SmjerInfo
                {
                    Id = x.Id,
                    SmjerNaziv = x.Naziv,
                    BrojStudenata = ctx.Studenti.Count(s => s.SmjerId == x.Id)
                }))
                .ToList();

            SmjerIndexVM model = new SmjerIndexVM
            {
                Smjerovi = smjerovi,
                FakultetId = fakultetId,
                FakultetNaziv = fakultet.Naziv
            };

            return View(model);
        }

        public ActionResult Dodaj(int fakultetId)
        {
            Fakultet fakultet = ctx.Fakulteti.Find(fakultetId);

            SmjerUrediVM model = new SmjerUrediVM
            {
                FakultetId = fakultetId, 
                FakultetNaziv = fakultet.Naziv
            };

            return View("Uredi", model);
        }

        public ActionResult Uredi(int? smjerId)
        {
            Smjer smjer = ctx.Smjerovi
                .Where(x=>x.Id == smjerId)
                .Include(x=>x.Fakultet).Single();

            SmjerUrediVM model = new SmjerUrediVM
            {
                Id = smjer.Id,
                SmjerNaziv = smjer.Naziv,
                FakultetId = smjer.FakultetId,
                FakultetNaziv = smjer.Fakultet.Naziv
            };

            return View(model);
        }

        public ActionResult Snimi(SmjerUrediVM smjer)
        {
            Smjer smjerDB;
            if (smjer.Id == 0)
            {
                smjerDB = new Smjer();
                ctx.Smjerovi.Add(smjerDB);
            }
            else
            {
                smjerDB = ctx.Smjerovi.Find(smjer.Id);
            }
            smjerDB.Naziv = smjer.SmjerNaziv;
            smjerDB.FakultetId = smjer.FakultetId;

            ctx.SaveChanges();

            return RedirectToAction("Index", new {fakultetId = smjer.FakultetId });
        }

        public ActionResult Obrisi(int smjerId)
        {
            Smjer smjer = ctx.Smjerovi.Find(smjerId);
            ctx.Smjerovi.Remove(smjer);
            ctx.SaveChanges();

            return RedirectToAction("Index", new { fakultetId = smjer.FakultetId });
        }
    }
}