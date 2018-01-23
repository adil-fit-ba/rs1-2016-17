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
using eAkademija.Web.Areas.Instruktor.ViewModels;

namespace eAkademija.Web.Controllers
{
    public class NavigationController : Controller
    {

        private MyContext _ctx = new MyContext();

        
        public ActionResult Index()
        {
            if (User.Identity.Name != null && User.Identity.Name != ""  && User.Identity.GetUserId() != null )
            {
                NavigationVM Model = new NavigationVM()
                {
                    Id = User.Identity.GetUserId()
                };
                Model.ImePrezime = _ctx.AppUserDbSet.Where(x=>x.Id == Model.Id).FirstOrDefault().Ime + " " + _ctx.AppUserDbSet.Where(x => x.Id == Model.Id).FirstOrDefault().Prezime;
                Model.PutanjaSlikeProfila = _ctx.AppUserDbSet.Where(x => x.Id == Model.Id).FirstOrDefault().ProfilePictureName;

                return PartialView("NavigationLoggedIn",Model);
            }
            else
            {
                return PartialView("Navigation");
            }

                
        }


        public ActionResult KategorijeNav()
        {

            var Model = new KursManageVM
            {
                KursKategorijaList = _ctx.KategorijaDbSet.ToList(),
                KursPotkategorijaList = _ctx.PotkategorijaDbSet.ToList()
            };

            return PartialView(Model);

        }
    }
}