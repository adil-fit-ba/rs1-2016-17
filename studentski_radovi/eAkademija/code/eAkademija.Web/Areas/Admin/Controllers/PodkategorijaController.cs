using eAkademija.Data.DAL;
using eAkademija.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eAkademija.Web.Areas.Admin.Controllers
{
    public class PodkategorijaController : Controller
    {
        private MyContext context = new MyContext();

        // GET: Admin/Podkategorija
        public ActionResult Index(int kategorijaId)
        {
            Kategorija kategorija = context.KategorijaDbSet.Where(x => x.Id == kategorijaId).FirstOrDefault();
            return View(kategorija);
        }

        public ActionResult Add(int Id)
        {
            ViewBag.ajaxLoad = "podkategorija_" + Id;
            ViewBag.categoryId = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Add(int categoryId, Potkategorija podkategorija)
        {
            Potkategorija nova = new Potkategorija
            {
                Naziv = podkategorija.Naziv,
                KategorijaId = categoryId
            };

            context.PotkategorijaDbSet.Add(nova);
            context.SaveChanges();

            Kategorija kategorija = context.KategorijaDbSet.Where(x => x.Id == categoryId).FirstOrDefault();
            return View("Index", kategorija);
        }

        public ActionResult Delete(int Id)
        {
            Potkategorija podkategorija = context.PotkategorijaDbSet.Where(x => x.Id == Id).FirstOrDefault();
            int KategorijaId = podkategorija.KategorijaId;
                
            context.PotkategorijaDbSet.Remove(podkategorija);
            context.SaveChanges();

            Kategorija kategorija = context.KategorijaDbSet.Where(x => x.Id == KategorijaId).FirstOrDefault();
            return View("Index", kategorija);
        }

        public ActionResult EmptyView()
        {
            return View();
        }
    }
}