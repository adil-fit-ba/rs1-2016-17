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
    public class NppController : Controller
    {
        private MojContext ctx = new MojContext();

        public ActionResult Index(int smjerId)
        {
            Smjer smjer = ctx.Smjerovi.Where(x => x.Id == smjerId).Include(x => x.Fakultet).Single();

            List<NppIndexVM.NppInfo> nppInfos = (ctx.NPPs
                .Where(x => x.SmjerId == smjerId)
                .Select(x => new NppIndexVM.NppInfo()
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    AkademskaGodina = x.AkademskaGodina.Opis,

                   
                }))
                .ToList();

            NppIndexVM model = new NppIndexVM
            {
                NPPs = nppInfos, 
                FakultetNaziv = smjer.Fakultet.Naziv,
                SmjerNaziv = smjer.Naziv,
                SmjerId = smjer.Id,
            };

            return View(model);
        }

        public ActionResult Dodaj(int smjerId)
        {
            Smjer smjer = ctx.Smjerovi.Where(x => x.Id == smjerId).Include(x => x.Fakultet).Single();

            NppUrediVM model = new NppUrediVM
            {
                AkademskaGodinaStavke = AkademskaGodinaStavke(),

                FakultetNaziv = smjer.Fakultet.Naziv,
                SmjerNaziv = smjer.Naziv,
                SmjerId = smjer.Id,
            };

            return View("Uredi", model);
        }

        public ActionResult Uredi(int nppId)
        {
            NPP npp = ctx.NPPs.Where(x=>x.Id == nppId).Include(x=>x.Smjer.Fakultet).Single();
            Smjer smjer = npp.Smjer;

            NppUrediVM model = new NppUrediVM
            {
                Id = npp.Id,
                Naziv = npp.Naziv,
                AkademskaGodinaId = npp.AkademskaGodinaId,
                AkademskaGodinaStavke = AkademskaGodinaStavke(),

                FakultetNaziv = smjer.Fakultet.Naziv,
                SmjerNaziv = smjer.Naziv,
                SmjerId = smjer.Id,
            };

            return View(model);
        }

        public ActionResult Snimi(NppUrediVM input)
        {
            NPP entity;
            if (input.Id == 0)
            {
                entity = new NPP();
                ctx.NPPs.Add(entity);
            }
            else
            {
                entity = ctx.NPPs.Find(input.Id);
            }
            entity.Naziv = input.Naziv;
            entity.SmjerId = input.SmjerId;
            entity.AkademskaGodinaId = input.AkademskaGodinaId;

            ctx.SaveChanges();

            return RedirectToAction("Index", new {smjerId = input.SmjerId});
        }

        public ActionResult Obrisi(int id)
        {
            NPP npp = ctx.NPPs.Find(id);
            ctx.NPPs.Remove(npp);
            ctx.SaveChanges();

            return RedirectToAction("Index", new { smjerId = npp.SmjerId });
        }

       private List<SelectListItem> AkademskaGodinaStavke()
        {
            List<SelectListItem> data = new List<SelectListItem>();
            data.Add(new SelectListItem
            {
                Value = null,
                Text = "(odaberite akademsku godinu)"
            });

            data.AddRange(ctx.AkademskeGodine
                .OrderBy(x => x.Opis)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Opis
                }).ToList());
            return data;
        }
    }
}