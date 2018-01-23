using eAkademija.Data.DAL;
using eAkademija.Data.Enums;
using eAkademija.Data.Model;
using eAkademija.Web.Areas.Admin.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eAkademija.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KursController : Controller
    {

        private MyContext _ctx = new MyContext();

        public ActionResult Index(string naziv = "", KursStatus arhiv_opcija = KursStatus.ACTIVE)    
        {
            IEnumerable<Kurs> kurseviZaArhivu = _ctx.KursDbSet.AsQueryable();
            kurseviZaArhivu = kurseviZaArhivu.Where(x => x.Status == arhiv_opcija);

            if (!string.IsNullOrWhiteSpace(naziv))
                kurseviZaArhivu = kurseviZaArhivu.Where(x => x.Naziv.Contains(naziv));
           

            List<KursViewModel> kurseviPrikaz = new List<KursViewModel>();
            foreach(var item in kurseviZaArhivu)
            {
                kurseviPrikaz.Add(new KursViewModel { Id = item.Id, DatumKreiranja = item.DatumKreiranja, Name = item.Naziv, Status = item.Status });
            }

            ViewBag.ArhivOpcija = arhiv_opcija;
            return View(kurseviPrikaz);
        }

        public ActionResult Show(int kursId)
        {
            Kurs kurs = _ctx.KursDbSet.Where(x => x.Id == kursId).FirstOrDefault();
            int brojKorisnika = _ctx.StudentKursDbSet.Where(x => x.KursId == kurs.Id).ToList().Count();

            KursDetaljiModel kursDetalji = new KursDetaljiModel
            {
                Naziv = kurs.Naziv,
                Opis = kurs.Opis,
                Datum = kurs.DatumKreiranja,
                brojPolaznika = brojKorisnika
            };

            return View(kursDetalji);
        }

        public ActionResult Archive(int kursId)
        {
            Kurs kursZaArhivu = _ctx.KursDbSet.Where(x => x.Id == kursId).FirstOrDefault();
            kursZaArhivu.Status = Data.Enums.KursStatus.ARCHIVED;
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EmptyView()
        {
            return View();
        }

        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }
    }
}