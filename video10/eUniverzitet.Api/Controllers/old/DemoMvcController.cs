using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Api.Controllers
{
    public class DemoMvcController : Controller
    {
        // GET: DemoMvc
        public ActionResult Index()
        {
           MojContext ctx = new MojContext();

            List<Student> students = ctx.Students.ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }
    }
}