using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eAkademija.Data.DAL;
using eAkademija.Data.Model;

namespace eAkademija.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            @ViewBag.Title = "Online platforma za učenje";
            return View();

        }
    }
}