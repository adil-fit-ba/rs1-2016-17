using System.Web.Mvc;
using eUniverzitet.Data.Models;
using eUniverzitet.Web.Helper;

namespace eUniverzitet.Web.Controllers
{
    public class HomeController : Controller
    {
        // Ovo je ulazna tačka za url http://adresa-servera/

        public ActionResult Index()
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);

            if (k == null)
                return RedirectToAction("Logout", "Autentifikacija", new { area = "" });

            if (k.Zaposlenik != null)
                return RedirectToAction("Index", "Home", new {area="ModulZaposlenik"});

            if (k.Student != null)
                return RedirectToAction("Index", "Home", new { area = "ModulStudenti" });

            return RedirectToAction("Logout", "Autentifikacija", new {area = ""});
        }


    

    }
}