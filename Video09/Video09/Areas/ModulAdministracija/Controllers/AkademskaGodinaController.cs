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
    public class AkademskaGodinaController : Controller
    {
        private MojContext ctx = new MojContext();

        public ActionResult Index()
        {
            List<AkademskaGodinaIndexVM.AkademskaGodinaInfo> akademskaGodinaInfos = ctx.AkademskeGodine.Select(x => new AkademskaGodinaIndexVM.AkademskaGodinaInfo
            {
                Id = x.Id,
                Opis = x.Opis
            }).ToList();

            AkademskaGodinaIndexVM model = new AkademskaGodinaIndexVM
            {
                AkademskeGodine = akademskaGodinaInfos
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
            AkademskaGodinaUrediVM model = ctx.AkademskeGodine
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
                ctx.AkademskeGodine.Add(entity);
            }
            else
            {
                entity = ctx.AkademskeGodine.Find(input.Id);
            }
            entity.Opis = input.Opis;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Obrisi(int id)
        {
            AkademskaGodina x = ctx.AkademskeGodine.Find(id);
            ctx.AkademskeGodine.Remove(x);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}