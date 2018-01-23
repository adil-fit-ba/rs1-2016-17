using eAkademija.Data.DAL;
using eAkademija.Data.Model;
using eAkademija.Web.Areas.Instruktor.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eAkademija.Web.Areas.Instruktor.Controllers
{
    [Authorize(Roles = "Instruktor")]
    public class ZadacaController : Controller
    {
        private MyContext _ctx = new MyContext();


        public ActionResult Index(int kursId)
        {
            List<Zadaca> model = _ctx.ZadacaDbSet.Where(y => y.KursId == kursId).OrderByDescending(orderby => orderby.Id).ToList();

            return View(model);
        }


        // TODO Metoda Zadaj
        public ActionResult Zadaj(int kursId)
        {
            string loggedInUserId = User.Identity.GetUserId();
            string instruktorId = _ctx.KursDbSet.Find(kursId).Instruktor.Id;

            if (loggedInUserId.Equals(instruktorId))
            {
                var model = new ZadacaManageVM
                {
                    ZadacaKursId = kursId,
                    ZadacaUraditiDo = DateTime.Now.AddDays(3)
                };

                return View("Manage", model);
            }
            else
            {
                var model = new GreskaVM
                {
                    OpisGreske = "Neispravna sesija ili niste ovlašteni za pregled ovog resursa!"
                };

                return View("Greska", model);
            }
        }


        public ActionResult Uredi(int id)
        {
            var model = _ctx.ZadacaDbSet.Where(y => y.Id == id).Select(x => new ZadacaManageVM
            {
                ZadacaId = x.Id,
                ZadacaKursId = x.Kurs.Id,
                ZadacaNaslov = x.Naslov,
                ZadacaOpis = x.Opis,
                ZadacaUkupnoBodova = x.UkupnoBodova,
                ZadacaUraditiDo = x.UraditiDo
            }).Single();

            return View("Manage", model);
        }


        public ActionResult Obrisi(int id, int kursId)
        {
            _ctx.ZadacaDbSet.Remove(_ctx.ZadacaDbSet.Find(id));

            _ctx.SaveChanges();

            return RedirectToAction("Index", "Zadaca", new { @kursId = kursId });
        }


        // TODO Metoda Snimi
        public ActionResult Snimi(ZadacaManageVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Manage", vm);
            }


            Zadaca zadaca;

            if(vm.ZadacaId == 0)
            {
                zadaca = new Zadaca();
                _ctx.ZadacaDbSet.Add(zadaca);
            }
            else
            {
                zadaca = _ctx.ZadacaDbSet.Find(vm.ZadacaId);
            }

            zadaca.KursId = vm.ZadacaKursId;
            zadaca.Naslov = vm.ZadacaNaslov;
            zadaca.Opis = vm.ZadacaOpis;
            zadaca.UkupnoBodova = vm.ZadacaUkupnoBodova;
            zadaca.UraditiDo = vm.ZadacaUraditiDo;

            // TODO Snimanje zapisa u DB za sve studente na Kursu

            if(vm.ZadacaId == 0)
            {
                foreach(var item in _ctx.StudentKursDbSet.Where(w => w.KursId == zadaca.KursId))
                {
                    _ctx.ZadacaStudentKursDbSet.Add(
                        new ZadacaStudentKurs
                        {
                            DatumNapisan = null,
                            Rjesenje = "",
                            Poeni = -1,
                            ZadacaId = zadaca.Id,
                            StudentKursId = item.Id
                        });
                }
            }

            _ctx.SaveChanges();

            return RedirectToAction("Index", "Zadaca", new { @kursId = zadaca.KursId });
        }


        public ActionResult Odustani(int kursId)
        {
            return RedirectToAction("Index", "Zadaca", new { @kursId = kursId });
        }

    }
}