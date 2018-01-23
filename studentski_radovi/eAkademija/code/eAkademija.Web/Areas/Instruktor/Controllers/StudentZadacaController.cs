using eAkademija.Data.DAL;
using eAkademija.Data.Model;
using eAkademija.Web.Areas.Instruktor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eAkademija.Web.Areas.Instruktor.Controllers
{
    [Authorize(Roles = "Instruktor")]
    public class StudentZadacaController : Controller
    {
        private MyContext _ctx = new MyContext();


        public ActionResult Index(int zadacaId)
        {
            //string id = _ctx.ZadacaDbSet.Find(zadacaId).Kurs.Instruktor.Id;


            var model = new StudentZadacaIndexVM
            {
                StudentZadace = _ctx.ZadacaStudentKursDbSet.Where(y => y.Zadaca.Id == zadacaId).Select(x => new StudentZadacaRow
                {
                    StudentZadacaId = x.Id,
                    StudentZadacaImeStudenta = x.StudentKurs.Student.AppUser.Ime + " " + x.StudentKurs.Student.AppUser.Prezime,
                    StudentZadacaDatumNapisan = x.DatumNapisan,
                    StudentZadacaRjesenje = x.Rjesenje,
                    StudentZadacaPoeni = x.Poeni,
                    StudentZadacaUkupnoBodova = x.Zadaca.UkupnoBodova,

                    StudentZadacaKursId = x.StudentKurs.Kurs.Id
                }).OrderByDescending(orderby => orderby.StudentZadacaDatumNapisan).ToList()
            };

            return View(model);
        }


        public ActionResult Ocijeni(int id)
        {
            var model = _ctx.ZadacaStudentKursDbSet.Where(y => y.Id == id).Select(x => new StudentZadacaManageVM
            {
                StudentZadacaId = x.Id,
                StudentZadacaZadacaId = x.Zadaca.Id,
                StudentZadacaPoeni = x.Poeni,
                StudentZadacaUkupnoBodova = x.Zadaca.UkupnoBodova

            }).Single();

            return View("Manage", model);
        }


        public ActionResult SnimiOcjenu(StudentZadacaManageVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Manage", vm);
            }


            ZadacaStudentKurs studentZadaca = _ctx.ZadacaStudentKursDbSet.Find(vm.StudentZadacaId);

            studentZadaca.Poeni = vm.StudentZadacaPoeni;

            _ctx.SaveChanges();

            return RedirectToAction("Index", "StudentZadaca", new { @zadacaId = vm.StudentZadacaZadacaId });
        }

    }

}