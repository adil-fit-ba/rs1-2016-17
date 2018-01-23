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
    public class PitanjeOdgovorController : Controller
    {
        private MyContext _ctx = new MyContext();


        public ActionResult Index(int kursId)
        {
            var model = new ViewModels.PitanjeOdgovor
            {
                PitanjaOdgovori = _ctx.PitanjeOdgovorDbSet.Where(y => y.StudentKurs.Kurs.Id == kursId).Select(x => new PitanjeOdgovorRow
                {
                    PitanjeOdgId = x.Id,

                    PitanjeOdgStudentImePrezime = x.StudentKurs.Student.AppUser.Ime + " " + x.StudentKurs.Student.AppUser.Prezime,
                    PitanjeOdgStudentSlikaPutanja = x.StudentKurs.Student.AppUser.ProfilePictureName,

                    // TODO rijesiti prikaz slike logovanog Instruktora

                    PitanjeOdgDatumPitanja = x.DatumPitanja,
                    PitanjeOdgPitanje = x.Pitanje,
                    PitanjeOdgDatumOdgovora = x.DatumOdgovora.Value,
                    PitanjeOdgOdgovor = x.Odgovor

                }).OrderByDescending(orderby => orderby.PitanjeOdgId).ToList()
            };

            return View(model);
        }


        public ActionResult Odgovori(int id)
        {
            var model = _ctx.PitanjeOdgovorDbSet.Where(y => y.Id == id).Select(x => new PitanjeOdgovorManageVM
            {
                PitanjeOdgovorId = x.Id,
                PitanjeOdgovorKursId = x.StudentKurs.Kurs.Id

            }).Single();

            return View("Manage", model);
        }


        public ActionResult SnimiOdgovor(PitanjeOdgovorManageVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Manage", vm);
            }

            Data.Model.PitanjeOdgovor odgovor = _ctx.PitanjeOdgovorDbSet.Find(vm.PitanjeOdgovorId);

            odgovor.DatumOdgovora = DateTime.Now;
            odgovor.Odgovor = vm.PitanjeOdgovorOdgovor;

            _ctx.SaveChanges();

            return RedirectToAction("Index", "PitanjeOdgovor", new { @kursId = vm.PitanjeOdgovorKursId });
        }

    }
}