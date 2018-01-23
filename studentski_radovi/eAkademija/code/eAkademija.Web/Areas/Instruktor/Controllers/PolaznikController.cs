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
    public class PolaznikController : Controller
    {
        private MyContext _ctx = new MyContext();


        public ActionResult Index(int kursId)
        {
            var model = new PolaznikIndexVM
            {
                Polaznici = _ctx.StudentKursDbSet.Where(w => w.Kurs.Id == kursId).Select(x => new PolaznikRow
                {
                    PolaznikId = x.Student.Id,
                    PolaznikPrezimeIme = x.Student.AppUser.Prezime + " " + x.Student.AppUser.Ime,
                    PolaznikDatumPocetka = x.DatumPocetak.Value,
                    PolaznikDatumZavrsetka = x.DatumKraj.Value,
                    PolaznikOcjena = x.Ocjena,

                    PolaznikSlikaPutanja = x.Student.AppUser.ProfilePictureName,

                    PolaznikPohadjaKursCount= _ctx.StudentKursDbSet.Where(w => w.Student.Id == x.Student.Id).Count()

                }).OrderBy(orderby => orderby.PolaznikPrezimeIme).ToList(),

                PolaznikKursId = kursId,
                PolaznikKursNaziv = _ctx.KursDbSet.Find(kursId).Naziv
            };


            return View("Index", model);
        }


        public ActionResult Ocijeni(int kursId, string polaznikId)
        {
            var model = _ctx.StudentKursDbSet.Where(w => w.Kurs.Id == kursId && w.Student.Id.Equals(polaznikId)).Select(x => new PolaznikManageVM
            {
                PolaznikId = x.Student.Id,
                PolaznikOcjena = x.Ocjena,
                PolaznikKursId = x.Kurs.Id

            }).Single();

            return View("Ocijeni", model);
        }


        public ActionResult SnimiOcjenu(PolaznikManageVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Ocijeni", vm);
            }

            StudentKurs polaznik = _ctx.StudentKursDbSet.Where(w => w.Student.Id.Equals(vm.PolaznikId)).FirstOrDefault();

            polaznik.Ocjena = vm.PolaznikOcjena;
            polaznik.DatumKraj = DateTime.Parse(DateTime.Now.ToShortDateString());

            _ctx.SaveChanges();

            return RedirectToAction("Index", "Polaznik", new { @kursId = vm.PolaznikKursId });
        }



    }
}