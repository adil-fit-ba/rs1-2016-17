using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MVC.Areas.ModulStudentskaSluzba.Models;
using MVC.DAL;
using MVC.Helper;
using MVC.Models;

namespace MVC.Areas.ModulStudentskaSluzba.Controllers
{
    [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Referent)]
    public class StudentController : Controller
    {
        private MojContext ctx = new MojContext();

        public ActionResult Index(int? smjeriId)
        {
            List<StudentIndexVM.StudentInfo> studentInfos = ctx.Studenti
                .Where(x => !smjeriId.HasValue || x.SmjerId == smjeriId)
                .Select(x => new StudentIndexVM.StudentInfo()
                {
                    BrojIndeksa = x.BrojIndeksa,
                    Fakultet_Naziv = x.Smjer.Fakultet.Naziv,
                    Id = x.Id,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    Smjer_Naziv = x.Smjer.Naziv,
                    EctsUkupno =
                        ctx.SlusaPredmet.Where(y => y.UpisGodine.StudentId == x.Id && y.FinalnaOcjena != null)
                            .Sum(z => (float?) z.Predaje.Predmet.Ects) ?? 0,
                    BrojPolozenihPredmeta =
                        ctx.SlusaPredmet.Count(y => y.UpisGodine.StudentId == x.Id && y.FinalnaOcjena != null)
                })
                .ToList();

            StudentIndexVM Model = new StudentIndexVM 
            {
                studenti = studentInfos, 
                smjeroviStavke = UcitajSmjerove()
            };

            return View("Index", Model);
        }

        public ActionResult Obrisi(int studentId)
        {
            Student x = ctx.Studenti.Find(studentId);
            ctx.Studenti.Remove(x);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Dodaj()
        {
            StudentUrediVM Model = new StudentUrediVM();

            Model.SmjerStavke = UcitajSmjerove();
            return View("Uredi", Model);
        }

        public ActionResult Uredi(int studentId)
        {
            Student student = ctx.Studenti
                .Where(x => x.Id == studentId)
                .Include(x => x.Korisnik)
                .Single();

            StudentUrediVM Model = new StudentUrediVM
            {
                Id = student.Id,
                Ime = student.Korisnik.Ime,
                Prezime = student.Korisnik.Prezime,
                KorisnickoIme = student.Korisnik.KorisnickoIme,
                Lozinka = student.Korisnik.Lozinka,
                SmjerId = student.SmjerId,
                BrojIndeksa = student.BrojIndeksa,
                DatumRodjenja = student.DatumRodjenja,
                SmjerStavke = UcitajSmjerove()
            };


            return View("Uredi", Model);
        }

        public ActionResult Snimi(StudentUrediVM student)
        {
            if (!ModelState.IsValid)
            {
                student.SmjerStavke = UcitajSmjerove();
                return View("Uredi", student);
            }

            Student studentDB;
            if (student.Id == 0)
            {
                studentDB = new Student();
                studentDB.Korisnik = new Korisnik();
                ctx.Studenti.Add(studentDB);
            }
            else
            {
                studentDB = ctx.Studenti.Where(s => s.Id == student.Id).Include(s => s.Korisnik).FirstOrDefault();
            }


            studentDB.Korisnik.Ime = student.Ime;
            studentDB.Korisnik.Prezime = student.Prezime;
            studentDB.Korisnik.KorisnickoIme = student.KorisnickoIme;
            studentDB.Korisnik.Lozinka = student.Lozinka;
            studentDB.DatumRodjenja = student.DatumRodjenja;
            studentDB.BrojIndeksa = student.BrojIndeksa;
            studentDB.SmjerId = student.SmjerId;


            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        private List<SelectListItem> UcitajSmjerove()
        {
            var smjerovi = new List<SelectListItem>();
            smjerovi.Add(new SelectListItem {Value = null, Text = "(svi smjerovi)"});

            smjerovi.AddRange(ctx.Smjerovi
                    .Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Fakultet.Naziv + " " + x.Naziv}).ToList());
            return smjerovi;
        }
    }
}