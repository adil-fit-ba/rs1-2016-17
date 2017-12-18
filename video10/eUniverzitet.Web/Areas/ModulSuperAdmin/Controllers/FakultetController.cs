using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;
using eUniverzitet.Web.Areas.ModulSuperAdmin.Models;
using eUniverzitet.Web.Helper;

namespace eUniverzitet.Web.Areas.ModulSuperAdmin.Controllers
{
    [Autorizacija(student: false, zaposlenikKorisnickaUlogas: new[] {KorisnickaUloga.SuperAdministrator})]
    public class FakultetController : Controller
    {
        private MojContext ctx = new MojContext();

        public ActionResult Index()
        {
            List<FakultetIndexVM.Info> fakultetInfos = ctx.Fakultets.Select(x => new FakultetIndexVM.Info
            {
                Id = x.Id,
                Naziv = x.Naziv,
                BrojOdsjeka = ctx.Odsjeks.Count(y => y.FakultetId == x.Id),
                BrojStudenata = ctx.Studiranjes.Count(y => y.Npp.FakultetId == x.Id),
                NaucnaOblast = x.NaucnaOblast.Opis

            }).ToList();

            FakultetIndexVM model = new FakultetIndexVM
            {
                TabelaPodaci = fakultetInfos
            };
            return View(model);
        }
    }

}