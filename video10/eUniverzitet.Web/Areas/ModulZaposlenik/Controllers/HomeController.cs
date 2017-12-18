using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;
using eUniverzitet.Web.Areas.ModulZaposlenik.Models;
using eUniverzitet.Web.Helper;

namespace eUniverzitet.Web.Areas.ModulZaposlenik.Controllers
{
  //  [Autorizacija(student: false, zaposlenikKorisnickaUlogas: new[] { KorisnickaUloga.SuperAdministrator, KorisnickaUloga.AdministratorInstitucije,})]
    public class HomeController : Controller
    {
        private MojContext ctx = new MojContext();

        public ActionResult Index()
        {
            Korisnik logiraniKorisnik = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            int zaposlenikId = logiraniKorisnik.Zaposlenik.Id;
            HomeIndexVM model = new HomeIndexVM();
            model.TabelaPodaci = ctx.Zaposlenjes.Where(x=>x.ZaposlenikId == zaposlenikId).Select( x=> new HomeIndexVM.ZaposlenjaInfo
            {
                ZaposlenjeId = x.Id,
                OrganizacionaJedinica = x.OrganizacionaJedinica.Naziv,
                RadnoMjestoVrsta = x.ZaposlenjeMjesto.Naziv,
                KorisnickaUloga = x.KorisnickaUloga,
                UgovorPocetak = x.UgovorPocetak
            }).ToList();


            return View(model);
        }

        public ActionResult WS()
        {
            int zaposlenikId = Autentifikacija.GetLogiraniKorisnik(HttpContext).Zaposlenik.Id;
            HomeIndexVM model = new HomeIndexVM();
            model.TabelaPodaci = ctx.Zaposlenjes.Where(x => x.ZaposlenikId == zaposlenikId).Select(x => new HomeIndexVM.ZaposlenjaInfo
            {
                ZaposlenjeId = x.Id,
                OrganizacionaJedinica = x.OrganizacionaJedinica.Naziv,
                RadnoMjestoVrsta = x.ZaposlenjeMjesto.Naziv,
                KorisnickaUloga = x.KorisnickaUloga,
                UgovorPocetak = x.UgovorPocetak
            }).ToList();


            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult OdabirZaposlenje(int zaposlenjeid)
        {
            Zaposlenje zaposlenje = ctx.Zaposlenjes.Find(zaposlenjeid);

            if (!ImaPravioOdabira(zaposlenje))
                return RedirectToAction("Index");

            switch (zaposlenje.KorisnickaUloga)
            {
                case KorisnickaUloga.SuperAdministrator: 
                    return RedirectToAction("Index", "Home", new {area = "ModulSuperAdmin"});

                case KorisnickaUloga.AdministratorInstitucije:
                    return RedirectToAction("Index", "Home", new { area = "ModulAdministracija"});

                case KorisnickaUloga.Edukator: 
                    return RedirectToAction("Index", "Home", new {area = "ModulEdukatori"});

                case KorisnickaUloga.StudentskaSluzba: 
                    return RedirectToAction("Index", "Home", new {area = "ModulStudentskaSluzba"});

                default:
                    return RedirectToAction("Index");
            }

           
        }

        private bool ImaPravioOdabira(Zaposlenje zaposlenje)
        {
            List<Zaposlenje> zaposlenjes = Autentifikacija.getZaposlenjes(HttpContext);

            if (zaposlenjes.Any(x=>x.KorisnickaUloga == KorisnickaUloga.SuperAdministrator))
                return true;

            if (zaposlenjes.Any(x => x.KorisnickaUloga == KorisnickaUloga.AdministratorInstitucije && x.OrganizacionaJedinicaId == zaposlenje.OrganizacionaJedinicaId))
                return true;

            if (zaposlenjes.Any(x => x.Id == zaposlenje.Id))
                return true;

            return false;
        }
    }
}