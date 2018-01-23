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
    [Authorize(Roles = "Student")]
    public class ZadacaController : Controller
    {
        private MyContext _ctx = new MyContext();

      
        public ActionResult Index(int kursId)
        {

            string loggedInUserId = User.Identity.GetUserId();
            List<Zadaca> podaci = _ctx.ZadacaDbSet.Where(y => y.KursId == kursId).OrderByDescending(orderby => orderby.Id).ToList();
            List<ZadacaVM> model = new List<ZadacaVM>();

            model.AddRange(podaci.Select(x => new ZadacaVM {
                
                ZadacaId = x.Id,
                KursId = x.KursId,
                Naslov = x.Naslov,
                Opis = x.Opis,
                Rjesenje = _ctx.ZadacaStudentKursDbSet.Where(y => y.ZadacaId == x.Id && y.StudentKursId ==
                                 (_ctx.StudentKursDbSet.Where(z => z.KursId == kursId && z.StudentId == loggedInUserId)
                                 .FirstOrDefault().Id)).FirstOrDefault()?.Rjesenje,
                UkupnoBodova = x.UkupnoBodova,
                UraditDo = x.UraditiDo,
                StudentKursId = _ctx.StudentKursDbSet.Where(y => y.KursId == kursId && y.StudentId == loggedInUserId).FirstOrDefault().Id,
                TrenutnoBodova = _ctx.ZadacaStudentKursDbSet.Where(y => y.ZadacaId == x.Id && y.StudentKursId ==
                                 (_ctx.StudentKursDbSet.Where(z => z.KursId == kursId && z.StudentId == loggedInUserId)
                                 .FirstOrDefault().Id)).FirstOrDefault()?.Poeni



            }));
 
            return View(model);
        }

        public ActionResult Ocjene(int zadacaId, int kursId)
        {
            string loggedInUserId = User.Identity.GetUserId();
            int studentKursId = _ctx.StudentKursDbSet.Where(x => x.KursId == kursId && x.StudentId == loggedInUserId).FirstOrDefault().Id;
            int brojBodova = _ctx.ZadacaStudentKursDbSet.Where(x => x.ZadacaId == zadacaId && x.StudentKursId == studentKursId).FirstOrDefault().Id;

            ViewBag.Ocjena = brojBodova.ToString();

            return PartialView("Ocjene");

        }

        public ActionResult Dodaj(int id)
        {

            string loggedInUserId = User.Identity.GetUserId();

         


            ZadacaVM Model = new ZadacaVM();
            int KursId = _ctx.ZadacaDbSet.Where(x => x.Id == id).FirstOrDefault().KursId;


            if (!_ctx.StudentKursDbSet.Where(x => x.KursId == KursId && x.StudentId == loggedInUserId).FirstOrDefault().DaLiJePrijavljen)
            {
                return JavaScript("window.location = '/FrontKurs/SingleKurs/" + KursId + "'");
            }



            Model.KursId = KursId;
            Model.ZadacaId = id;
            Model.StudentKursId = _ctx.StudentKursDbSet.Where(x => x.KursId == KursId && x.StudentId == loggedInUserId).FirstOrDefault().Id;
            Model.DatumNapisan = (_ctx.ZadacaStudentKursDbSet.Where(x => x.ZadacaId == id && x.StudentKursId == Model.StudentKursId).FirstOrDefault()?.DatumNapisan)??DateTime.Now;
            Model.Rjesenje = _ctx.ZadacaStudentKursDbSet.Where(x => x.ZadacaId == id && x.StudentKursId == Model.StudentKursId).FirstOrDefault()?.Rjesenje??"" ;
            Model.StudentZadacaId = _ctx.ZadacaStudentKursDbSet.Where(x => x.ZadacaId == id && x.StudentKursId == Model.StudentKursId).FirstOrDefault()?.Id??0;
            return PartialView("Dodaj", Model);
        }


        public ActionResult Obrisi(int id, int kursId)
        {
            _ctx.ZadacaDbSet.Remove(_ctx.ZadacaDbSet.Find(id));

            _ctx.SaveChanges();

            return RedirectToAction("Index", "Zadaca", new { @kursId = kursId });
        }


        // TODO Metoda Snimi
        public ActionResult Snimi(ZadacaVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Dodaj", vm);
            }


            ZadacaStudentKurs zadaca;

            if (vm.StudentZadacaId == 0)
            {
                zadaca = new ZadacaStudentKurs();
                _ctx.ZadacaStudentKursDbSet.Add(zadaca);
                zadaca.Poeni = -1;
            }
            else
            {
                zadaca = _ctx.ZadacaStudentKursDbSet.Find(vm.StudentZadacaId);
            }

            
            zadaca.ZadacaId = vm.ZadacaId;
            zadaca.Rjesenje = vm.Rjesenje;
            zadaca.DatumNapisan = DateTime.Now;
            zadaca.StudentKursId = (int)vm.StudentKursId;


            // TODO Snimanje zapisa u DB za sve studente na Kursu


            _ctx.SaveChanges();

            return RedirectToAction("Index", "Zadaca", new { @kursId = vm.KursId });
        }


        public ActionResult Odustani(int kursId)
        {
            return RedirectToAction("Index", "Zadaca", new { @kursId = kursId });
        }

    }
}