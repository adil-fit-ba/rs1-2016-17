using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eAkademija.Web.Models;
using eAkademija.Data.Model;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using eAkademija.Data.DAL;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Net;

namespace eAkademija.Web.Controllers
{   
    [Authorize]
    public class AccountController : Controller
    {
        private MyContext _ctx = new MyContext();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Update", "Account");
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(loginModel.Username, loginModel.Password);
                if(user == null)
                {
                    ModelState.AddModelError("", "Username ili password nisu tacni, pokusajte ponovo.");
                    ViewBag.returnUrl = returnUrl;
                }
                else
                {
                    ClaimsIdentity identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect("/Home/Index");
                    }

                    return Redirect(returnUrl);
                }
            }

            return View(loginModel);
        }




        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> LoginBar(LoginModel loginModel, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AppUser user = await UserManager.FindAsync(loginModel.Username, loginModel.Password);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("", "Username ili password nisu tacni, pokusajte ponovo.");
               
        //        }
        //        else
        //        {
        //            ClaimsIdentity identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //            AuthManager.SignOut();
        //            AuthManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
       
        //        }
        //    }

        //    return Redirect("/Home/Index");
        //}





        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {

            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Update", "Account");

            IEnumerable<Grad> gradovi = _ctx.Gradovi;
            ViewBag.gradovi = gradovi;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> Register(RegisterModel model, HttpPostedFileBase profile_picture)
        {

            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Update", "Account");

            if (ModelState.IsValid)
            {
                string path = "";
                string pic = "";
                if(profile_picture != null)
                {
                    pic = System.IO.Path.GetFileName(profile_picture.FileName);
                    path = System.IO.Path.Combine(
                    Server.MapPath("~/profile_images"), pic);
                    // file is uploaded
                    profile_picture.SaveAs(path);
                }

                AppUser user = new AppUser {

                        Ime = model.Ime,
                        Prezime =model.Prezime,
                        UserName = model.Username,
                        Email = model.Email,
                        GradId = model.GradId,
                        hasContentBan = false,
                        ProfilePictureName = pic,
                        Instruktor = model.Tip == "Instruktor" ? new Instruktor() : null,
                        Student = model.Tip == "Student" ? new Student() : null
                };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {                  
                    UserManager.AddToRole(user.Id, model.Tip);

                    /*
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = new Uri(Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }
                    , protocol: Request.Url.Scheme));
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Hello " + user.UserName + ", please confirm your account by clicking <a href = " + callbackUrl + "> Here </a>");

                    return RedirectToAction("EmailConfirmMessage", "Account");
                    */

                    return View("RegistracijaUspjeh");
                }
            }

            IEnumerable<Grad> gradovi = _ctx.Gradovi;
            ViewBag.gradovi = gradovi;

            return View(model);

        }

        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public ActionResult Update()
        {
            AppUser user = UserManager.FindById(User.Identity.GetUserId());
            IEnumerable<SelectListItem> selectGradovi = _ctx.Gradovi.Select(x => new SelectListItem { Text = x.Naziv, Value = x.Id.ToString()});
            ViewBag.gradovi = selectGradovi;
            return PartialView(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(AppUser userModel, HttpPostedFileBase profile_picture)
        {

            if (ModelState.IsValid)
            {
                string path = "";
                string pic = "";
                AppUser user = UserManager.FindById(User.Identity.GetUserId());
                if (profile_picture != null)
                {
                    pic = DateTime.Now.ToString("yyyyMMddHHmmss") + System.IO.Path.GetFileName(profile_picture.FileName);
                    path = System.IO.Path.Combine(
                    Server.MapPath("~/profile_images"), pic);
                    // file is uploaded
                    profile_picture.SaveAs(path);
                    user.ProfilePictureName = "/profile_images/" + pic;
                }



                if (userModel.Ime != null)
                    user.Ime = userModel.Ime;

                if (userModel.Prezime != null)
                    user.Prezime = userModel.Prezime;

                if (userModel.Ime != null)
                    user.Email = userModel.Email;

                if (userModel.Ime != null)
                    user.GradId = userModel.GradId;




                if(userModel.PasswordHash !=null) { 
                var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                var resulttwo = await UserManager.ResetPasswordAsync(user.Id, token, userModel.PasswordHash);
                }

                IdentityResult result = UserManager.Update(user);

                if (result.Succeeded)
                {
                    TempData["ResponseMessage"] = "Uspješno ste editovali profil.";
                }
                else
                {
                    TempData["ResponseMessage"] = "Podaci koji ste unjeli nisu ispravni. Pokušajte ponovo";
                }

            }
            return RedirectToAction("Update");
        }


        [AllowAnonymous]
        [HttpGet]
        public PartialViewResult Username()
        {
            AppUser user = UserManager.FindById(User.Identity.GetUserId());
            if(user == null)
            {
                ViewBag.username = "";
            }else
            {
                ViewBag.username = user.UserName;
            }

            return PartialView();
        }

        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
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
    }
}