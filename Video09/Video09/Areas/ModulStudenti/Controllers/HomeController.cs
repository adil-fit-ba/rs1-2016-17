using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.ModulStudenti.Controllers
{
    public class HomeController : Controller
    {
        // GET: ModulStudenti/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}