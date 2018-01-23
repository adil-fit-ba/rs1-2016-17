using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Areas.ModulAdministracija.Models;

namespace MVC.Areas.ModulAdministracija.Controllers
{
    public class TestEditorController : Controller
    {
        // GET: ModulAdministracija/TestKalkulator
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Operacija1(string tekst, string naslov)
        {
           
                int d = tekst.Length;
                return View("Snimljeno", d);
        }
    }
}