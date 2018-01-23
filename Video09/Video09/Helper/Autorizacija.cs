using System.Collections.Generic;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Helper
{
    public class Autorizacija:FilterAttribute, IAuthorizationFilter
    {
        private readonly KorisnickeUloge[] _dozvoljeneUloge;

        public Autorizacija(params KorisnickeUloge[] dozvoljeneUloge)
        {
            _dozvoljeneUloge = dozvoljeneUloge;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(filterContext.HttpContext);

            if (k == null)
            {
                filterContext.HttpContext.Response.Redirect("/Login");
                return;
            }

            //provjera
            foreach (KorisnickeUloge x in _dozvoljeneUloge)
            {
                if (x == KorisnickeUloge.Administrator && k.Administrator != null)
                    return;
                if (x == KorisnickeUloge.Nastavnik && k.Nastavnik != null)
                    return;
                if (x == KorisnickeUloge.Referent && k.Referent != null)
                    return;
                if (x == KorisnickeUloge.Student && k.Student != null)
                    return;
            }
            //ako funkcija nije prekinuta pomoću "return", onda korisnik nema pravo pistupa pa će se vršiti redirekcija na "/Home/Index"
            filterContext.HttpContext.Response.Redirect("/");
        }
    }
    public enum KorisnickeUloge
    {
        Nastavnik,
        Referent,
        Student,
        Administrator
    }
}