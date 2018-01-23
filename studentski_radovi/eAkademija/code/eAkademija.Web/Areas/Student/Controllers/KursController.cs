using eAkademija.Data.DAL;
using eAkademija.Data.Model;
using eAkademija.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using eAkademija.Web.Areas.Student.ViewModels;

namespace eAkademija.Web.Areas.Student.Controllers
{
    public class KursController : Controller
    {

        private MyContext _ctx = new MyContext();
        // GET: Student/Kurs

        public ActionResult PrikaziDetalje (int id)
        {

            string userID = User.Identity.GetUserId();
            float? KkursOcjena = _ctx.KursLajkDbSet.Where(x => x.StudentKurs.KursId == id).Select(x => x.Ocjena).DefaultIfEmpty().Sum();
            Kurs podaci = _ctx.KursDbSet.Where(x => x.Id == id).FirstOrDefault();
            int KategorijaId = _ctx.KategorizacijaDbSet.Where(x => x.KursId == id).FirstOrDefault().PotkategorijaId;
            int skId = _ctx.StudentKursDbSet.Where(x => x.KursId == id && x.StudentId == userID).FirstOrDefault().Id;
            int count = _ctx.KursLajkDbSet.Where(x => x.StudentKurs.KursId == id).Count();
            Front_Kurs model = new Front_Kurs()
            {
                Id = podaci.Id,
                KursKategorija = _ctx.PotkategorijaDbSet.Where(x => x.Id == KategorijaId).FirstOrDefault().Naziv,
                Instruktor = podaci.Instruktor.AppUser.Ime,
                KursNaziv = podaci.Naziv,
                KursOpis = podaci.Opis,
                KursDatumKreiranja = podaci.DatumKreiranja,
                KursKursNivo = podaci.Nivo,
                KursVideoId = podaci.VideoId,
                KursThumbImagePutanja = "http://img.youtube.com/vi/" + podaci.VideoId + "/maxresdefault.jpg",
                KursStatus = podaci.Status,
                JeUFavoritima = _ctx.StudentKursDbSet.Where(x => x.KursId == id && x.StudentId == userID).FirstOrDefault()?.DaLiJeFavorit,
                StudentKursId = _ctx.StudentKursDbSet.Where(x => x.KursId == id && x.StudentId == userID).FirstOrDefault()?.Id,
                StudentOcjena = _ctx.KursLajkDbSet.Where(x => x.StudentKursId == skId).FirstOrDefault()?.Ocjena,
                KursOcjena = (int)_ctx.KursLajkDbSet.Where(x => x.StudentKurs.KursId == id).Select(x => x.Ocjena).DefaultIfEmpty().Sum() / (count== 0 ? 1: count),
                IsPrijavljenNaKurs = _ctx.StudentKursDbSet.Where(x => x.KursId == id && x.StudentId == userID).FirstOrDefault() != null ? true : false,
                KursPolazniciCount = _ctx.StudentKursDbSet.Where(y => y.Kurs.Id == podaci.Id).Count()
            };

            ViewBag.Title = model.KursNaziv;

            return PartialView(model);
        }

        public ActionResult PrikaziStatistiku (int id)
        {

            List<StatistikaVM> Model = new List<StatistikaVM>();

            var kursevi = _ctx.StudentKursDbSet.Where(x => x.KursId == id).ToList();

            List<KursLajk> podaci = new List<KursLajk>();


            foreach(var studentkurs in kursevi)
            {
                foreach( var lajk in _ctx.KursLajkDbSet.ToList())
                {
                    if(lajk.StudentKursId == studentkurs.Id)
                    {
                        podaci.Add(lajk);
                    }

                }

            }

          

            Model.AddRange(podaci.Select(x => new StatistikaVM {

                Id = x.Id,
                ImePolaznika = _ctx.StudentKursDbSet.Where(y=>y.Id == x.StudentKursId).FirstOrDefault().Student.AppUser.Ime,
                Ocjena = x.Ocjena

            }));


             return View(Model);
        }
        
        public ActionResult OcjeniKurs (int kursId, int ocjena)
        {


             

            string studentId = User.Identity.GetUserId();

            if(!_ctx.StudentKursDbSet.Where(x=>x.KursId == kursId && x.StudentId == studentId).FirstOrDefault().DaLiJePrijavljen)
            {
                return JavaScript("window.location = '/FrontKurs/SingleKurs/"+kursId+"'");
            }


            int skId = _ctx.StudentKursDbSet.Where(x => x.KursId == kursId && x.StudentId == studentId).FirstOrDefault().Id;

            KursLajk obj = _ctx.KursLajkDbSet.Where(x => x.StudentKursId == skId).FirstOrDefault();

            if(obj != null)
                obj.Ocjena = ocjena;
            else
            {
                obj = new KursLajk();
                obj.StudentKursId = skId;
                obj.DatumLajkan = DateTime.Now;
                obj.Ocjena = ocjena;
                _ctx.KursLajkDbSet.Add(obj);
            }
            _ctx.SaveChanges();



            //return new HttpStatusCodeResult(HttpStatusCode.OK);
            return RedirectToAction("PrikaziStatistiku", new { @id = kursId });

        }

        [Authorize]
        public ActionResult Prijava(int kursId, string userId)
        {



            StudentKurs obj = _ctx.StudentKursDbSet.Where(x => x.KursId == kursId && x.StudentId == userId).FirstOrDefault();

            if(obj != null)
            {
                obj.DaLiJePrijavljen = true;
                _ctx.SaveChanges();

            }
            else
            {
                obj = new StudentKurs();
                obj.KursId = kursId;
                obj.StudentId = userId;
                obj.Ocjena = 0;
                obj.DatumPocetak = DateTime.Now;
                obj.DatumKraj = null;
                obj.DaLiJeFavorit = false;
                obj.DaLiJePrijavljen = true;
                _ctx.StudentKursDbSet.Add(obj);
                _ctx.SaveChanges();
            }
            return RedirectToAction("SingleKurs","FrontKurs", new { area="", id = kursId });
        }

        [ValidateAntiForgeryToken]
        public ActionResult OdjaviKurs (int studentKursId, bool? mojikursevi)
        {



            StudentKurs obj = _ctx.StudentKursDbSet.Where(x => x.Id == studentKursId).FirstOrDefault();
            obj.DaLiJePrijavljen = false;
            obj.DaLiJeFavorit = false;
            _ctx.SaveChanges();
            int? kursId = obj.KursId;

            if(mojikursevi.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return RedirectToAction("SingleKurs", "FrontKurs", new { area = "", id = kursId });
            }





        }

        [ValidateAntiForgeryToken]
        public ActionResult DodajFavorit(int StudentKursId)
        {


            if (!_ctx.StudentKursDbSet.Where(x => x.Id == StudentKursId).FirstOrDefault().DaLiJePrijavljen)
            {
                int? kursId = _ctx.StudentKursDbSet.Where(x => x.Id == StudentKursId).FirstOrDefault().KursId;
                return JavaScript("window.location = '/FrontKurs/SingleKurs/" + kursId + "'");
            }



            StudentKurs obj = _ctx.StudentKursDbSet.Where(x => x.Id == StudentKursId).FirstOrDefault();

            obj.DaLiJeFavorit = true;

            _ctx.SaveChanges();


            return new HttpStatusCodeResult(HttpStatusCode.OK);

        }
        public ActionResult UkloniFavorit(int StudentKursId)
        {

            StudentKurs obj = _ctx.StudentKursDbSet.Where(x => x.Id == StudentKursId).FirstOrDefault();

            obj.DaLiJeFavorit = false;

            _ctx.SaveChanges();

            return RedirectToAction("PrikaziFavorite");

        }

        public ActionResult PrikaziFavorite()
        {


            List<Front_Kurs> Model = new List<Front_Kurs>();
            string studentId = User.Identity.GetUserId();




            List<StudentKurs> studentKursevi = _ctx.StudentKursDbSet.Where(x => x.StudentId == studentId && x.DaLiJeFavorit == true).ToList();
            List<Kurs> kurseviFavoriti = new List<Kurs>();
            foreach (var sk in studentKursevi)
            {

                foreach (var k in _ctx.KursDbSet.Where(x => !x.Status.ToString().Equals("ARCHIVED")).OrderByDescending(x => x.DatumKreiranja).ToList())
                {

                    if (k.Id == sk.KursId)
                        kurseviFavoriti.Add(k);

                }


            }


            Model.AddRange(kurseviFavoriti.Select(x => new Front_Kurs
            {

                Id = x.Id,
                Instruktor = x.Instruktor.AppUser.Ime,
                KursNaziv = x.Naziv,
                KursOpis = x.Opis,
                KursDatumKreiranja = x.DatumKreiranja,
                KursKursNivo = x.Nivo,
                KursVideoId = x.VideoId,
                KursThumbImagePutanja = "http://img.youtube.com/vi/" + x.VideoId + "/mqdefault.jpg",
                KursStatus = x.Status,
                StudentKursId = _ctx.StudentKursDbSet.Where(y=>y.StudentId == studentId && y.KursId == x.Id).FirstOrDefault()?.Id,
                KursPolazniciCount = _ctx.StudentKursDbSet.Where(y => y.Kurs.Id == x.Id).Count()

            }));


            return PartialView(Model);
        }

        public ActionResult PrikaziMojeKurseve()
        {


            List<Front_Kurs> Model = new List<Front_Kurs>();
            string studentId = User.Identity.GetUserId();




            List<StudentKurs> studentKursevi = _ctx.StudentKursDbSet.Where(x => x.StudentId == studentId && x.DaLiJePrijavljen == true).ToList();
            List<Kurs> mojiKursevi = new List<Kurs>();
            foreach (var sk in studentKursevi)
            {

                foreach (var k in _ctx.KursDbSet.Where(x => !x.Status.ToString().Equals("ARCHIVED")).OrderByDescending(x => x.DatumKreiranja).ToList())
                {

                    if (k.Id == sk.KursId)
                        mojiKursevi.Add(k);

                }


            }


            Model.AddRange(mojiKursevi.Select(x => new Front_Kurs
            {

                Id = x.Id,
                Instruktor = x.Instruktor.AppUser.Ime,
                KursNaziv = x.Naziv,
                KursOpis = x.Opis,
                KursDatumKreiranja = x.DatumKreiranja,
                KursKursNivo = x.Nivo,
                KursVideoId = x.VideoId,
                KursThumbImagePutanja = "http://img.youtube.com/vi/" + x.VideoId + "/mqdefault.jpg",
                KursStatus = x.Status,
                StudentKursId = _ctx.StudentKursDbSet.Where(y => y.StudentId == studentId && y.KursId == x.Id).FirstOrDefault()?.Id,
                KursPolazniciCount = _ctx.StudentKursDbSet.Where(y => y.Kurs.Id == x.Id).Count()

            }));


            return PartialView(Model);
        }

    }
}