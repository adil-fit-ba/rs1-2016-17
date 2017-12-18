using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;
using eUniverzitet.Web.Areas.ModulSuperAdmin.Models;
using eUniverzitet.Web.Helper;

namespace eUniverzitet.Web.Areas.ModulSuperAdmin.Controllers
{
        [Autorizacija(student: false, zaposlenikKorisnickaUlogas: new[] { KorisnickaUloga.SuperAdministrator })]
    public class AkademskaGodinaController : Controller
    {
        private MojContext ctx = new MojContext();

        public ActionResult Index()
        {
            List<AkademskaGodinaIndexVM.AkademskaGodinaInfo> akademskaGodinaInfos = ctx.AkademskaGodinas.Select(x => new AkademskaGodinaIndexVM.AkademskaGodinaInfo
            {
                Id = x.Id,
                Opis = x.Opis
            }).ToList();

            AkademskaGodinaIndexVM model = new AkademskaGodinaIndexVM
            {
                TabelaPodaci = akademskaGodinaInfos
            };
            return View(model);
        }

         public ActionResult Dodaj()
        {
            AkademskaGodinaUrediVM model = new AkademskaGodinaUrediVM();

            return View("Uredi", model);
        }

        public ActionResult Uredi(int id)
        {
            AkademskaGodinaUrediVM model = ctx.AkademskaGodinas
                .Where(x => x.Id == id)
                .Select(x => new AkademskaGodinaUrediVM
                {
                    Id = x.Id,
                    Opis = x.Opis
            }).Single();

            return View(model);
        }

        public ActionResult Snimi(AkademskaGodinaUrediVM input)
        {
            AkademskaGodina entity;
            if (input.Id == 0)
            {
                entity = new AkademskaGodina();
                ctx.AkademskaGodinas.Add(entity);
            }
            else
            {
                entity = ctx.AkademskaGodinas.Find(input.Id);
            }
            entity.Opis = input.Opis;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Obrisi(int id)
        {
            AkademskaGodina x = ctx.AkademskaGodinas.Find(id);
            ctx.AkademskaGodinas.Remove(x);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}