using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;
using eUniverzitet.Web.Helper;

namespace eUniverzitet.Web.Controllers
{
    public class AutentifikacijaController : Controller
    {
        private MojContext ctx = new MojContext();
        public ActionResult Index()
        {
            // Vraća view iz Views\Login\Index.cshtml
            return View();
        }

        public ActionResult Provjera(string username, string password, string zapamti)
        {
            Korisnik k = ctx.Korisniks
                .Include(x=>x.Students.Select(y=>y.Studiranjes))
                .Include(x=>x.Zaposleniks.Select(y=>y.Zaposlenja))
                .SingleOrDefault(x => x.KorisnickoIme == username && x.Lozinka == password);

            if (k == null)
            {
                return RedirectToAction("Index");
            }

            Autentifikacija.PokreniNovuSesiju(k, HttpContext, (zapamti == "on"));

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Autentifikacija.PokreniNovuSesiju(null, HttpContext, true);
            return RedirectToAction("Index");
        }
    }
}