using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eAkademija.Data.Enums;
using eAkademija.Data.Model;
using eAkademija.Data.DAL;

namespace eAkademija.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KategorijaController : Controller
    {

        private MyContext context = new MyContext();

        // GET: Admin/Kategorija
        public ActionResult Index(KategorijaStatus status = KategorijaStatus.ACTIVE)
        {

            IEnumerable<Kategorija> kategorije = context.KategorijaDbSet.AsQueryable();
            kategorije = kategorije.Where(x => x.KategorijaStatus == status);

            ViewBag.status = status;
            return View(kategorije);
        }

        public ActionResult IndexAjax(string naziv = "", KategorijaStatus status = KategorijaStatus.ACTIVE)
        {

            IEnumerable<Kategorija> kategorije = context.KategorijaDbSet.AsQueryable();
            kategorije = kategorije.Where(x => x.KategorijaStatus == status);

            if (!string.IsNullOrWhiteSpace(naziv))
            {
                kategorije.Where(x => x.Naziv.Contains(naziv));
            }

            ViewBag.status = status;
            return View(kategorije);
        }


        public ActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Add(Kategorija kategorijaPost)
        {
            Kategorija kategorija = new Kategorija { Naziv = kategorijaPost.Naziv, KategorijaStatus = KategorijaStatus.ACTIVE };
            context.KategorijaDbSet.Add(kategorija);
            context.SaveChanges();

            IEnumerable<Kategorija> kategorije = context.KategorijaDbSet.Where(x => x.KategorijaStatus == KategorijaStatus.ACTIVE);

            return View("IndexAjax", kategorije);
        }

        public ActionResult Delete(int categoryId, KategorijaStatus status)
        {
            Kategorija kategorija = context.KategorijaDbSet.Where(x => x.Id == categoryId).FirstOrDefault();
            if(kategorija != null)
            {
                context.KategorijaDbSet.Remove(kategorija);
                context.SaveChanges();
            }

            IEnumerable<Kategorija> kategorije = context.KategorijaDbSet.Where(x => x.KategorijaStatus == status);
            ViewBag.status = status;
            return View("IndexAjax", kategorije);

        }

        public ActionResult UpdateStatus(int categoryId)
        {
            Kategorija kategorija = context.KategorijaDbSet.Where(x => x.Id == categoryId).FirstOrDefault();
            if (kategorija != null)
            {
                if(kategorija.KategorijaStatus.Equals(KategorijaStatus.ACTIVE))
                {
                    kategorija.KategorijaStatus = KategorijaStatus.FLAGGED_FOR_PERMISSION;
                }else
                {
                    kategorija.KategorijaStatus = KategorijaStatus.ACTIVE;
                }

                context.SaveChanges();
            }

            IEnumerable<Kategorija> kategorije = context.KategorijaDbSet.Where(x => x.KategorijaStatus == KategorijaStatus.ACTIVE);
            return View("IndexAjax", kategorije);

        }
    }
}