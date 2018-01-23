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

namespace eAkademija.Web.Controllers
{
    public class FrontKursController : Controller
    {

        private MyContext ctx = new MyContext();
        // GET: FrontKurs
        public ActionResult Index()
        {
            // Listaj latest kurseve
            List<Kurs> podaci = ctx.KursDbSet.Where(x => !x.Status.ToString().Equals("ARCHIVED")).OrderByDescending(x=>x.DatumKreiranja).ToList();
            List<Front_Kurs> Model = new List<Front_Kurs>();
            string studentId = User.Identity.GetUserId();


            Model.AddRange(podaci.Select(x => new Front_Kurs {

                Id = x.Id,
                Instruktor = x.Instruktor.AppUser.Ime,
                KursNaziv = x.Naziv,
                KursOpis = x.Opis,
                KursDatumKreiranja = x.DatumKreiranja,
                KursKursNivo = x.Nivo,
                KursVideoId = x.VideoId,
                KursThumbImagePutanja = "http://img.youtube.com/vi/" + x.VideoId + "/mqdefault.jpg",
                KursStatus = x.Status,
                KursPolazniciCount = ctx.StudentKursDbSet.Where(y => y.Kurs.Id == x.Id).Count()

            }));

            ViewBag.NazivKategorije = "kursevi";


            return PartialView("~/Views/FrontKurs/Index.cshtml", Model);
        }

        public ActionResult ListKategorije (int kategorijaId)
        {

            // Listaj latest kurseve
            List<Potkategorija> PodKategorije = ctx.PotkategorijaDbSet.Where(x => x.KategorijaId == kategorijaId).ToList();



            List<Kategorizacija> Kategorizacija = new List<Kategorizacija>();

            foreach(var pk in PodKategorije)
            {
                foreach (var kk in ctx.KategorizacijaDbSet.Where(x=>x.PotkategorijaId == pk.Id).ToList())
                { 
                Kategorizacija obj = kk;
                if(obj != null)
                {
                    Kategorizacija.Add(obj);
                }
                }
            }

            List<Kurs> podaci = new List<Kurs>();
            List<Front_Kurs> Model = new List<Front_Kurs>();


            foreach(var k in Kategorizacija)
            {
                Kurs obj = ctx.KursDbSet.Where(x => x.Id == k.KursId && !x.Status.ToString().Equals("ARCHIVED")).FirstOrDefault();

                if(obj != null)
                {
                    podaci.Add(obj);
                }
            }

            List<Kurs> bezduplikata = podaci.Distinct().OrderBy(x => x.DatumKreiranja).Take(4).ToList();

            string nazivKategorije = ctx.KategorijaDbSet.Where(x => x.Id == kategorijaId).FirstOrDefault().Naziv;

            Model.AddRange(bezduplikata.Select(x => new Front_Kurs
            {

                Id = x.Id,
                KursNaziv = x.Naziv,
                Instruktor = x.Instruktor.AppUser.Ime,
                KursOpis = x.Opis,
                KursDatumKreiranja = x.DatumKreiranja,
                KursKursNivo = x.Nivo,
                KursVideoId = x.VideoId,
                KursThumbImagePutanja = "http://img.youtube.com/vi/" + x.VideoId + "/mqdefault.jpg",
                KursStatus = x.Status,
                KursPolazniciCount = ctx.StudentKursDbSet.Where(y => y.Kurs.Id == x.Id).Count()

            }));

            ViewBag.NazivKategorije = "kursevi iz " + "\""  + nazivKategorije + "\"";

            return PartialView("~/Views/FrontKurs/Index.cshtml", Model);


        }

        public ActionResult SingleKurs(int id)
        {
            string userID = User.Identity.GetUserId();
            
            Kurs podaci = ctx.KursDbSet.Where(x => x.Id == id).FirstOrDefault();
            int KategorijaId = ctx.KategorizacijaDbSet.Where(x => x.KursId == id).FirstOrDefault().PotkategorijaId;
            Front_Kurs model = new Front_Kurs()
            {
                Id = podaci.Id,
                KursKategorija = ctx.PotkategorijaDbSet.Where(x=>x.Id == KategorijaId).FirstOrDefault().Naziv,
                Instruktor = podaci.Instruktor.AppUser.Ime,
                KursNaziv = podaci.Naziv,
                KursOpis = podaci.Opis,
                KursDatumKreiranja = podaci.DatumKreiranja,
                KursKursNivo = podaci.Nivo,
                KursVideoId = podaci.VideoId,
                KursThumbImagePutanja = "http://img.youtube.com/vi/" + podaci.VideoId + "/maxresdefault.jpg",
                KursStatus = podaci.Status,
                JeUFavoritima = ctx.StudentKursDbSet.Where(x => x.KursId == id && x.StudentId == userID).FirstOrDefault()?.DaLiJeFavorit,
                StudentKursId = ctx.StudentKursDbSet.Where(x => x.KursId == id && x.StudentId == userID).FirstOrDefault()?.Id,
                KursOcjena = ctx.StudentKursDbSet.Where(y => y.KursId == id && y.StudentId == userID).FirstOrDefault()?.Ocjena,
                IsPrijavljenNaKurs = ctx.StudentKursDbSet.Where(x=>x.KursId == id && x.StudentId == userID).FirstOrDefault()?.DaLiJePrijavljen??false,
                KursPolazniciCount = ctx.StudentKursDbSet.Where(y => y.Kurs.Id == podaci.Id).Count()
            };

            ViewBag.Title = model.KursNaziv;

            return PartialView("~/Views/FrontKurs/SingleKurs.cshtml",model);
        }

        public ActionResult SveKategorijePage()
        {



            List<Kurs> podaci = ctx.KursDbSet.Where(x=>!x.Status.ToString().Equals("ARCHIVED")).OrderByDescending(x=>x.Naziv).ToList();
            List<Front_Kurs> Model = new List<Front_Kurs>();



            Model.AddRange(podaci.Select(x => new Front_Kurs
            {

                Id = x.Id,
                KursNaziv = x.Naziv,
                Instruktor = x.Instruktor.AppUser.Ime,
                KursOpis = x.Opis,
                KursDatumKreiranja = x.DatumKreiranja,
                KursKursNivo = x.Nivo,
                KursVideoId = x.VideoId,
                KursThumbImagePutanja = "http://img.youtube.com/vi/" + x.VideoId + "/mqdefault.jpg",
                KursStatus = x.Status,
                KursPolazniciCount = ctx.StudentKursDbSet.Where(y => y.Kurs.Id == x.Id).Count()

            }));

            ViewBag.NazivKategorije = "Sve kategorije";



            return View("~/Views/FrontKurs/KategorijaPage.cshtml",Model);
        }






        public ActionResult KategorijaPage(int kategorijaId)
        {

            // Listaj latest kurseve
            List<Potkategorija> PodKategorije = ctx.PotkategorijaDbSet.Where(x => x.KategorijaId == kategorijaId).ToList();



            List<Kategorizacija> Kategorizacija = new List<Kategorizacija>();

            foreach (var pk in PodKategorije)
            {
                foreach (var kk in ctx.KategorizacijaDbSet.Where(x => x.PotkategorijaId == pk.Id).ToList())
                {
                    Kategorizacija obj = kk;
                    if (obj != null)
                    {
                        Kategorizacija.Add(obj);
                    }
                }
            }

            List<Kurs> podaci = new List<Kurs>();
            List<Front_Kurs> Model = new List<Front_Kurs>();


            foreach (var k in Kategorizacija)
            {
                Kurs obj = ctx.KursDbSet.Where(x => x.Id == k.KursId && !x.Status.ToString().Equals("ARCHIVED")).FirstOrDefault();

                if (obj != null)
                {
                    podaci.Add(obj);
                }
            }

            List<Kurs> bezduplikata = podaci.Distinct().OrderBy(x => x.DatumKreiranja).ToList();

            string nazivKategorije = ctx.KategorijaDbSet.Where(x => x.Id == kategorijaId).FirstOrDefault().Naziv;

            Model.AddRange(bezduplikata.Select(x => new Front_Kurs
            {

                Id = x.Id,
                KursNaziv = x.Naziv,
                Instruktor = x.Instruktor.AppUser.Ime,
                KursOpis = x.Opis,
                KursDatumKreiranja = x.DatumKreiranja,
                KursKursNivo = x.Nivo,
                KursVideoId = x.VideoId,
                KursThumbImagePutanja = "http://img.youtube.com/vi/" + x.VideoId + "/mqdefault.jpg",
                KursStatus = x.Status,
                KursPolazniciCount = ctx.StudentKursDbSet.Where(y => y.Kurs.Id == x.Id).Count()

            }));

            ViewBag.NazivKategorije = nazivKategorije;



            return View(Model);  
        }




        public ActionResult PodKategorijaPage (int kategorijaId, int podKategorijaId)
        {


            // Listaj latest kurseve
            List<Potkategorija> PodKategorije = ctx.PotkategorijaDbSet.Where(x => x.KategorijaId == kategorijaId).ToList();



            List<Kategorizacija> Kategorizacija = new List<Kategorizacija>();

      
                foreach (var kk in ctx.KategorizacijaDbSet.Where(x => x.PotkategorijaId == podKategorijaId).ToList())
                {
                    Kategorizacija obj = kk;
                    if (obj != null)
                    {
                        Kategorizacija.Add(obj);
                    }
                }
        

            List<Kurs> podaci = new List<Kurs>();
            List<Front_Kurs> Model = new List<Front_Kurs>();


            foreach (var k in Kategorizacija)
            {
                Kurs obj = ctx.KursDbSet.Where(x => x.Id == k.KursId && !x.Status.ToString().Equals("ARCHIVED")).FirstOrDefault();

                if (obj != null)
                {
                    podaci.Add(obj);
                }
            }

            List<Kurs> bezduplikata = podaci.Distinct().OrderBy(x => x.DatumKreiranja).ToList();

            string nazivKategorije = ctx.PotkategorijaDbSet.Where(x => x.Id == podKategorijaId).FirstOrDefault().Naziv;

            Model.AddRange(bezduplikata.Select(x => new Front_Kurs
            {

                Id = x.Id,
                KursNaziv = x.Naziv,
                Instruktor = x.Instruktor.AppUser.Ime,
                KursOpis = x.Opis,
                KursDatumKreiranja = x.DatumKreiranja,
                KursKursNivo = x.Nivo,
                KursVideoId = x.VideoId,
                KursThumbImagePutanja = "http://img.youtube.com/vi/" + x.VideoId + "/mqdefault.jpg",
                KursStatus = x.Status,
                KursPolazniciCount = ctx.StudentKursDbSet.Where(y => y.Kurs.Id == x.Id).Count()

            }));

            ViewBag.NazivKategorije = nazivKategorije;



            return View("~/Views/FrontKurs/KategorijaPage.cshtml", Model);
        }


        public ActionResult SearchResult(string kurseviSearch, int? podkategorijaId)
        {

            var Model = ctx.KursDbSet.Where(x => x.Naziv.Equals(kurseviSearch) && !x.Status.ToString().Equals("ARCHIVED")).FirstOrDefault();

            if(Model != null)
                 return RedirectToAction("SingleKurs", new {@id=Model.Id });

           List<Kurs> podaci = null; ;


            if (podkategorijaId.HasValue)
            {
                podaci = new List<Kurs>();
              

                foreach(var kategorizacija in ctx.KategorizacijaDbSet.Where(x=>x.PotkategorijaId == podkategorijaId).ToList())
                {
                    Kurs obj = null;
                    //if empty string
                    if (kurseviSearch=="")
                    {
                    obj = ctx.KursDbSet.Where(x => x.Id == kategorizacija.KursId && !x.Status.ToString().Equals("ARCHIVED")).FirstOrDefault();
                    }
                    else
                    { 
                    obj = ctx.KursDbSet.Where(x => x.Id == kategorizacija.KursId && !x.Status.ToString().Equals("ARCHIVED") && x.Naziv.ToLower().Contains(kurseviSearch.ToLower())).FirstOrDefault();
                    }


                    if (obj != null)
                    {
                        podaci.Add(obj);
                    }

                }

            }
            else
            { 
                podaci = ctx.KursDbSet.Where(x => x.Naziv.ToLower().Contains(kurseviSearch.ToLower())&& !x.Status.ToString().Equals("ARCHIVED")).ToList();
                if (podaci.Count == 0 && kurseviSearch == "")
                    podaci = ctx.KursDbSet.Where(x => !x.Status.ToString().Equals("ARCHIVED")).ToList();
            }

            


            List<Front_Kurs> ModelList = new List<Front_Kurs>();



            ModelList.AddRange(podaci.Select(x => new Front_Kurs
            {

                Id = x.Id,
                KursNaziv = x.Naziv,
                Instruktor = x.Instruktor.AppUser.Ime,
                KursOpis = x.Opis,
                KursDatumKreiranja = x.DatumKreiranja,
                KursKursNivo = x.Nivo,
                KursVideoId = x.VideoId,
                KursThumbImagePutanja = "http://img.youtube.com/vi/" + x.VideoId + "/mqdefault.jpg",
                KursStatus = x.Status,
                KursPolazniciCount = ctx.StudentKursDbSet.Where(y => y.Kurs.Id == x.Id).Count()

            }));

            var ModelPot = ctx.PotkategorijaDbSet.ToList();
  
 

            if(kurseviSearch != "")
            {
                ViewBag.RezultatPretrage = "za pojam pretrage: '" + kurseviSearch + "'";
            }
           
            ViewBag.SearchPojam = kurseviSearch;
            ViewBag.ModelPot = ModelPot;


            return View(ModelList);


        }



        public JsonResult KurseviSearch(string term)
        {


            List<string> searchresult = ctx.KursDbSet.Where(x=>x.Naziv.ToLower().Contains(term.ToLower()) && !x.Status.ToString().Equals("ARCHIVED")).Select(x=>x.Naziv).Take(5).ToList();



            return Json(searchresult, JsonRequestBehavior.AllowGet);

        }


    }
}