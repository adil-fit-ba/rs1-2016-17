using eAkademija.Data.DAL;
using eAkademija.Web.Areas.Instruktor.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace eAkademija.Web.Areas.Instruktor.Controllers
{
    [Authorize(Roles = "Instruktor")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}