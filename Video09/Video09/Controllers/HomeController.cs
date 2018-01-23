using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MVC.Helper;
using MVC.Models;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        // Ovo je ulazna tačka za url http://adresa-servera/
     
        public ActionResult Index()
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(HttpContext);
            if (k == null)
                return Redirect("/Login");


            return View(k);
        }
    }
}