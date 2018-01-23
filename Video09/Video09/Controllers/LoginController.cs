using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MVC.DAL;
using MVC.Helper;
using MVC.Models;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        private MojContext ctx = new MojContext();
        
        public ActionResult Index()
        {
            // Vraća view iz Views\Login\Index.cshtml
            return View();
        }

        public ActionResult Provjera(string username, string password, string zapamti)
        {
            Korisnik k = ctx.Korisnici
                .Include(x=>x.Nastavnik)
                .Include(x=>x.Student)
                .Include(x=>x.Administrator)
                .Include(x=>x.Referent)
                .SingleOrDefault(x => x.KorisnickoIme == username && x.Lozinka == password);

            if (k == null)
            {
                return Redirect("/Login");
            }

            Autentifikacija.PokreniNovuSesiju(k, HttpContext, (zapamti == "on"));
           
            return Redirect("/Home");
        }

        public ActionResult Logout()
        {
            Autentifikacija.PokreniNovuSesiju(null, HttpContext, true);
            return RedirectToAction("Index");
        }
    }
}