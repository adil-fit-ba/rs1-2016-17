using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.ModulAdministracija.Controllers
{
    public class HomeController : Controller
    {
        // GET: ModulAdministracija/HomeAdministracija
        public ActionResult Index()
        {
            return View();
        }
    }
}