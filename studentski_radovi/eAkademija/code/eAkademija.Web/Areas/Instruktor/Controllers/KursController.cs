using eAkademija.Data.DAL;
using eAkademija.Data.Enums;
using eAkademija.Data.Model;
using eAkademija.Web.Areas.Instruktor.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace eAkademija.Web.Areas.Instruktor.Controllers
{
    [Authorize(Roles = "Instruktor")]
    public class KursController : Controller
    {

        private MyContext _ctx = new MyContext();
        

        public ActionResult Index()
        {

            string loggedInUserId = User.Identity.GetUserId();


            var model = new KursIndexVM
            {
                Kursevi = _ctx.KursDbSet.Where(y => y.Instruktor.Id.Equals(loggedInUserId) && !y.Status.ToString().Equals("ARCHIVED")).Select(x => new KursRow
                {
                    KursId = x.Id,
                    KursNaziv = x.Naziv,
                    KursOpis = x.Opis,
                    KursDatumKreiranja = x.DatumKreiranja,
                    KursKursNivo = x.Nivo,
                    KursVideoId = x.VideoId,
                    KursThumbImagePutanja = "http://img.youtube.com/vi/"+x.VideoId+"/mqdefault.jpg",
                    KursStatus = x.Status,

                    KursPolazniciCount = _ctx.StudentKursDbSet.Where(y => y.Kurs.Id == x.Id).Count()

                }).OrderByDescending(orderBy => orderBy.KursDatumKreiranja).ToList(),
                
            };

            return View(model);
        }


        public ActionResult Pregled(int id)
        {
            string loggedInUserId = User.Identity.GetUserId();
            string instruktorId = _ctx.KursDbSet.Find(id).Instruktor.Id;
            
            if (loggedInUserId.Equals(instruktorId))
            {
                var model = _ctx.KursDbSet.Where(y => y.Id == id).Select(x => new KursPregledVM
                {
                    KursId = x.Id,
                    KursNaziv = x.Naziv,
                    KursDatumKreiranja = x.DatumKreiranja,
                    KursOpis = x.Opis,
                    KursVideoId = x.VideoId,
                    KursNivo = x.Nivo,
                    KursStatus = x.Status,

                    KursOcjenaCount = _ctx.KursLajkDbSet.Where(y => y.StudentKurs.Kurs.Id == id).Count(),
                    KursOcjenaAvg = _ctx.KursLajkDbSet.Where(y => y.StudentKurs.Kurs.Id == id).Select(s => s.Ocjena).Average(),

                    KursZadacaCount = _ctx.ZadacaDbSet.Where(y => y.Kurs.Id == id).Count(),
                    KursPitanjeOdgovorCount = _ctx.PitanjeOdgovorDbSet.Where(y => y.StudentKurs.Kurs.Id == id).Count()

                }).Single();

                return View(model);
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

        // TODO Doraditi sve atribute (Dodaj)
        public ActionResult Dodaj()
        {
            var model = new KursManageVM
            {
                KursDatumKreiranja = DateTime.Now,
                KursKategorijaList = _ctx.KategorijaDbSet.ToList(),
                KursPotkategorijaList = _ctx.PotkategorijaDbSet.ToList()
            };

            return View("Manage", model);
        }


        // TODO Doraditi sve atribute (Uredi)
        public ActionResult Uredi(int id)
        {
            string loggedInUserId = User.Identity.GetUserId();
            string instruktorId = _ctx.KursDbSet.Find(id).Instruktor.Id;

            if (loggedInUserId.Equals(instruktorId))
            {

                var model = _ctx.KursDbSet.Where(y => y.Id == id).Select(x => new KursManageVM
                {
                    KursId = x.Id,
                    KursNaziv = x.Naziv,
                    KursDatumKreiranja = x.DatumKreiranja,
                    KursOpis = x.Opis,
                    KursVideoId = x.VideoId,
                    KursStatus = x.Status,
                    KursNivo = x.Nivo,

                    KursKategorijaList = _ctx.KategorijaDbSet.ToList(),
                    KursPotkategorijaList = _ctx.PotkategorijaDbSet.ToList(),

                    KursKategorizacija = _ctx.KategorizacijaDbSet.Where(w => w.Kurs.Id == x.Id).Select(s => s.Potkategorija.Id).ToList()

                }).Single();

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

        // TODO Metoda Snimi
        public ActionResult Snimi(KursManageVM vm)
        {
            Kurs kurs;
            List<int> postedInts = vm.KursKategorizacija;
            List<int> dbInts = _ctx.KategorizacijaDbSet.Where(w => w.Kurs.Id == vm.KursId).Select(s => s.Potkategorija.Id).ToList();


            // TODO implementirati kompletnu validaciju
            if (!ModelState.IsValid)
            {
                vm.KursKategorijaList = _ctx.KategorijaDbSet.ToList();
                vm.KursPotkategorijaList = _ctx.PotkategorijaDbSet.ToList();
                vm.KursKategorizacija = (dbInts != null) ? dbInts : new List<int>();

                return View("Manage", vm);
            }
            
            if(vm.KursId == 0)
            {
                kurs = new Kurs();
                _ctx.KursDbSet.Add(kurs);
            }
            else
            {
                kurs = _ctx.KursDbSet.Find(vm.KursId);
            }

            // TODO doraditi atribute

            kurs.DatumKreiranja = vm.KursDatumKreiranja;
            kurs.Naziv = vm.KursNaziv;
            kurs.Opis = vm.KursOpis;
            kurs.VideoId = vm.KursVideoId;
            kurs.Nivo = vm.KursNivo.Value;
            string loggedInUserId = User.Identity.GetUserId();
            kurs.InstruktorId = loggedInUserId;
            //atribut STATUS nije potrebno dodati

            SacuvajKategorije(vm, postedInts, dbInts);

            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        
        public ActionResult Pretrazi()
        {
            Dictionary<string, string> formdata = new Dictionary<string, string>();
            foreach(string key in Request.Form.AllKeys)
            {
                formdata.Add(key, Request.Form[key]);
            }

            string naziv = "";
            formdata.TryGetValue("KursPretragaNaziv", out naziv);

            string opis = "";
            formdata.TryGetValue("KursPretragaOpis", out opis);

            
            string nivoString = "";
            formdata.TryGetValue("KursPretragaKursNivo", out nivoString);

            int nivoInt = -1;
            if (nivoString != "") { Int32.TryParse(nivoString, out nivoInt); }


            string statusString = "";
            formdata.TryGetValue("KursPretragaKursStatus", out statusString);

            int statusInt = -1;
            if (statusString != "") { Int32.TryParse(statusString, out statusInt); }


            string datumOd = "";
            formdata.TryGetValue("KursPretragaDatumOd", out datumOd);

            string datumDo = "";
            formdata.TryGetValue("KursPretragaDatumDo", out datumDo);


            string izostaviBezPolaznikaString = "";
            formdata.TryGetValue("KursPretragaIzostaviBezPolaznika", out izostaviBezPolaznikaString);


            // za razliku od query-ja na Index metodu, ovdje nema filtriranja 
            // za kurseve koji imaju status ARCHIVED = 2, iz razloga sto 
            // filter forma ima i dropdown listu sa svim statusima

            string loggedInUserId = User.Identity.GetUserId();

            var query = _ctx.KursDbSet.Where(kurs => kurs.Instruktor.Id.Equals(loggedInUserId));


            // polje: naziv
            naziv = naziv.ToLower();
            query = query.Where(kurs => kurs.Naziv.ToLower().Contains(naziv));

            // polje: opis
            opis = opis.ToLower();
            query = query.Where(kurs => kurs.Opis.ToLower().Contains(opis));

            // polje: nivo
            if (nivoInt != -1) { query = query.Where(kurs => kurs.Nivo == (KursNivo)nivoInt); }

            // polje: status
            if (statusInt != -1) { query = query.Where(kurs => kurs.Status == (KursStatus)statusInt); }


            DateTime? postedDatumOd = null;
            if (datumOd != null && datumOd != "")
            {
                postedDatumOd = DateTime.Parse(datumOd);
                query = query.Where(kurs => kurs.DatumKreiranja >= postedDatumOd);
            }


            DateTime? postedDatumDo = null;
            if (datumDo != null && datumDo != "")
            {
                postedDatumDo = DateTime.Parse(datumDo);
                query = query.Where(kurs => kurs.DatumKreiranja <= postedDatumDo);
            }



            // polje: [checkbox] Ne prikazuj kurseve bez polaznika?
            bool propIzostaviBezPolaznika = false;

            if (izostaviBezPolaznikaString != null && izostaviBezPolaznikaString.Equals("doIzostaviBezPolaznika"))
            {
                query = query.Where(kurs => _ctx.StudentKursDbSet.Where(x => x.Kurs.Id == kurs.Id).Count() > 0);

                propIzostaviBezPolaznika = true;
            }

            var model = new KursIndexVM
            {
                Kursevi = query.Select(x => new KursRow
                {
                    KursId = x.Id,
                    KursNaziv = x.Naziv,
                    KursOpis = x.Opis,
                    KursDatumKreiranja = x.DatumKreiranja,
                    KursVideoId = x.VideoId,
                    KursThumbImagePutanja = "http://img.youtube.com/vi/" + x.VideoId + "/mqdefault.jpg",
                    KursKursNivo = x.Nivo,
                    KursStatus = x.Status,

                    KursPolazniciCount = _ctx.StudentKursDbSet.Where(y => y.Kurs.Id == x.Id).Count()

                }).OrderByDescending(orderBy => orderBy.KursDatumKreiranja).ToList(),

                // atributi pretrage
                KursPretragaNaziv = naziv,
                KursPretragaOpis = opis,

                KursPretragaKursNivo = (KursNivo)nivoInt,
                KursPretragaKursStatus = (KursStatus)statusInt,

                KursPretragaDatumOd = (datumOd != null && datumOd != "" ? postedDatumOd : null),
                KursPretragaDatumDo = (datumDo != null && datumDo != "" ? postedDatumDo : null),

                KursPretragaIzostaviBezPolaznika = propIzostaviBezPolaznika

            };

            return View("Index", model);
        }

        
        public Boolean SacuvajKategorije(KursManageVM vm, IEnumerable<int> nove, IEnumerable<int> postojece)
        {
            if(postojece != null)
            {
                // ima postojecih kategorija u db za ovaj kurs
                foreach (var id in nove.ToArray().Except(postojece.ToArray()))
                    _ctx.KategorizacijaDbSet.Add(new Kategorizacija { KursId = vm.KursId, PotkategorijaId = id });

                foreach (var id in postojece.ToArray().Except(nove.ToArray()))
                {
                    Kategorizacija kategorija = _ctx.KategorizacijaDbSet.Where(w => w.Kurs.Id == vm.KursId && w.Potkategorija.Id == id).Single();
                    _ctx.KategorizacijaDbSet.Remove(kategorija);
                }
            }
            else
            {
                // nema postojecih kategorija u db za ovaj kurs (novi kurs)
                foreach (var id in nove.ToArray())
                    _ctx.KategorizacijaDbSet.Add(new Kategorizacija { KursId = vm.KursId, PotkategorijaId = id });
            }


            return true;
        }


        // TODO (opcionalno) Implementirati toggle button flag-unflag
        public ActionResult Arhiviraj(int id)
        {
            Kurs kurs = _ctx.KursDbSet.Find(id);
            kurs.Status = Data.Enums.KursStatus.FLAGGED_FOR_ARCHIVING;

            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult PrikaziPolaznike(int kursId)
        {
            string loggedInUserId = User.Identity.GetUserId();
            string instruktorId = _ctx.KursDbSet.Find(kursId).Instruktor.Id;

            if (loggedInUserId.Equals(instruktorId))
            {
                var model = _ctx.KursDbSet.Where(y => y.Id == kursId).Select(x => new KursPregledVM
                {
                    KursId = x.Id,
                    KursNaziv = x.Naziv

                }).Single();

                return View("Polaznici", model);
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
    }
}