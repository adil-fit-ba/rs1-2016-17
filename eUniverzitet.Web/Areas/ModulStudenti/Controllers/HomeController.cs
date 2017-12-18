using System;
using System.Web.Mvc;
using eUniverzitet.Web.Helper;

namespace eUniverzitet.Web.Areas.ModulStudenti.Controllers
{
    [Autorizacija(true)]
    public class HomeController : Controller
    {
        // GET: ModulStudenti/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OdabirStudiranje(int studiranjeId)
        {
            throw new NotImplementedException();
        }
    }
}