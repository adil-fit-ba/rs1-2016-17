using eAkademija.Data.DAL;
using eAkademija.Data.Model;
using eAkademija.Web.Areas.Student.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace eAkademija.Web.Areas.Student.Controllers
{
    public class PitanjeOdgovorController : Controller
    {

        private MyContext _ctx = new MyContext();


        public ActionResult Index(int kursId)
        {

            string Instruktor = _ctx.KursDbSet.Where(x => x.Id == kursId).FirstOrDefault().Instruktor.AppUser.Ime + " " + _ctx.KursDbSet.Where(x => x.Id == kursId).FirstOrDefault().Instruktor.AppUser.Prezime;
            string InstruktorPutanja = _ctx.KursDbSet.Where(x => x.Id == kursId).FirstOrDefault().Instruktor.AppUser.ProfilePictureName;

            PitanjeOdgovorVM model = new PitanjeOdgovorVM
            {
                PitanjaOdgovori = _ctx.PitanjeOdgovorDbSet.Where(y => y.StudentKurs.Kurs.Id == kursId).Select(x => new PitanjeOdgovorRow
                {
                    PitanjeOdgId = x.Id,

                    PitanjeOdgStudentImePrezime = x.StudentKurs.Student.AppUser.Ime + " " + x.StudentKurs.Student.AppUser.Prezime,
                    PitanjeOdgStudentSlikaPutanja = x.StudentKurs.Student.AppUser.ProfilePictureName,
                    
                    PitanjeOdgInstruktorImePrezime = Instruktor,
                    PitanjeOdgInstruktorSlikaPutanja = InstruktorPutanja,



                    // TODO rijesiti prikaz slike logovanog Instruktora

                    PitanjeOdgDatumPitanja = x.DatumPitanja,
                    PitanjeOdgPitanje = x.Pitanje,
                    PitanjeOdgDatumOdgovora = x.DatumOdgovora.Value,
                    PitanjeOdgOdgovor = x.Odgovor

                }).OrderByDescending(orderby => orderby.PitanjeOdgId).ToList()
            };

            return View(model);
        }


        public ActionResult SnimiPitanje(PitanjeOdgovorPostavi model)
        {

            PitanjeOdgovor obj = new PitanjeOdgovor();

            obj.StudentKursId = model.StudentKursId;
            obj.Pitanje = model.Pitanje;
            obj.DatumPitanja = model.DatumPitanja;
            _ctx.PitanjeOdgovorDbSet.Add(obj);
            _ctx.SaveChanges();

            ViewBag.UspjesnoPoslatoPitanje = "Vaše pitanje je postavljeno, uskoro će neko od instruktora da vam odgovori na isto.";

            //var model = _ctx.PitanjeOdgovorDbSet.Where(y => y.Id == id).Select(x => new PitanjeOdgovorVM
            //{
            //    PitanjeOdgovorId = x.Id,
            //    PitanjeOdgovorKursId = x.StudentKurs.Kurs.Id

            //}).Single();

            return RedirectToAction("Index", new { @kursId = model.KursId });
        }


        public ActionResult PostaviPitanje (int kursId, string studentID)
        {

            PitanjeOdgovorPostavi model = new PitanjeOdgovorPostavi();

            model.StudentKursId = _ctx.StudentKursDbSet.Where(x => x.KursId == kursId && x.StudentId == studentID).FirstOrDefault().Id;
            model.DatumPitanja = DateTime.Now;
            model.KursId = kursId;

            return View(model);

        }




    }
}