using eAkademija.Data.DAL;
using eAkademija.Data.Model;
using eAkademija.Web.Areas.Admin.Models;
using eAkademija.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eAkademija.Web.Areas.Admin.Controllers
{   
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {

        private MyContext _ctx = new MyContext();

        public ActionResult Index(string naziv = "", string tip = "")
        {  
            IEnumerable<AppUser> Users = UserManager.Users.Where(x => x.hasContentBan == false).ToList();

            IEnumerable<UserViewModel> UsersModel = convertUsers(Users);
            if (!string.IsNullOrWhiteSpace(tip))
                UsersModel = UsersModel.Where(x => x.Tip == tip);

            if (!string.IsNullOrWhiteSpace(naziv))
                UsersModel = UsersModel.Where(x => (x.Ime != null && x.Prezime != null) && x.Ime.Contains(naziv));

            ViewBag.Tip = tip;
            return View(UsersModel);
        }

        public ActionResult Show(string userId = "")
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction("Index");
            }

            AppUser user = UserManager.FindById(userId);
            
            if(user.Grad == null)
            {
                return RedirectToAction("Index");
            }

            string Tip = GetUserRole(user);
            ShowViewModel viewModel = new ShowViewModel
            {
                Ime = user.Ime,
                Prezime = user.Prezime,
                Username = user.UserName,
                Email = user.Email,
                Tip = Tip,
                Grad = user.Grad.Naziv,
                Kursevi = GetKursevi(user, Tip)             
            };

            return View(viewModel);
        }

        public ActionResult Delete(string userId)
        {
            AppUser user = UserManager.Users.Where(x => x.Id == userId).FirstOrDefault();
            user.hasContentBan = true;
            UserManager.Update(user);
            return RedirectToAction("Index");
        }

        public ActionResult EmptyView()
        {
            return View();
        }

        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        private IEnumerable<UserViewModel> convertUsers(IEnumerable<AppUser> Users)
        {
            List<UserViewModel> UsersForView = new List<UserViewModel>();
            foreach (var item in Users)
            {
                string gradNaziv = "";
                    
                if(_ctx.Gradovi.Where(x => x.Id == item.GradId).FirstOrDefault() == null)
                {
                    gradNaziv = "Grad nije upisan.";
                }else
                {
                    gradNaziv = _ctx.Gradovi.Where(x => x.Id == item.GradId).FirstOrDefault().Naziv;
                }    

                string tip = "";
                
                if (UserManager.IsInRole(item.Id, "Administrator"))
                {
                    tip = "Administrator";
                }
                else if (UserManager.IsInRole(item.Id, "Student"))
                {
                    tip = "Student";
                }
                else
                {
                    tip = "Instruktor";
                }

                UserViewModel model = new UserViewModel
                {   Id = item.Id,
                    Ime = item.Ime,
                    Prezime = item.Prezime,
                    Grad = gradNaziv,
                    Tip = tip
                };

                UsersForView.Add(model);
            }

            return UsersForView;
        }

        private string GetUserRole(AppUser user)
        {
            if (UserManager.IsInRole(user.Id, "Instruktor"))
                return "Instruktor";
            else if (UserManager.IsInRole(user.Id, "Student"))
                return "Student";
            else
                return "Administrator";
        }

        private List<Kurs> GetKursevi(AppUser user, string Tip)
        {
            List<Kurs> kursevi = new List<Kurs>();

            if(Tip == "Instruktor")
            {
                foreach(var kurs in user.Instruktor.Kursevi)
                {
                    kursevi.Add(kurs);
                }
                return kursevi;
            }else if(Tip == "Student")
            {
                foreach(var kurs in user.Student.StudentKurs)
                {
                    kursevi.Add(kurs.Kurs);
                }
                return kursevi;
            }else
            {
                return kursevi;
            }
        }
    }
}