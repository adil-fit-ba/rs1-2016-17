using System.Web.Mvc;
using eUniverzitet.Data.Models;
using eUniverzitet.Web.Helper;

namespace eUniverzitet.Web.Areas.ModulSuperAdmin.Controllers
{
        [Autorizacija(student: false, zaposlenikKorisnickaUlogas: new[] { KorisnickaUloga.SuperAdministrator })]
    public class HomeController : Controller
    {
        // GET: ModulAdministracija/HomeAdministracija
        public ActionResult Index()
        {
            return View();
        }
    }
}