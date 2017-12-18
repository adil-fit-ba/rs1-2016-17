using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Web.Helper
{
    public class Autorizacija:FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _student;
        private readonly bool _allZaposleniks;
        private readonly KorisnickaUloga[] _zaposlenikKorisnickaUlogas;

        public Autorizacija(bool student,  params KorisnickaUloga[] zaposlenikKorisnickaUlogas)
        {
            _student = student;
            _zaposlenikKorisnickaUlogas = zaposlenikKorisnickaUlogas;
        }

        public Autorizacija(bool student, bool allZaposleniks)
        {
            _student = student;
            _allZaposleniks = allZaposleniks;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Korisnik k = Autentifikacija.GetLogiraniKorisnik(filterContext.HttpContext);

            if (k == null)
            {
                filterContext.HttpContext.Response.Redirect("/Autentifikacija");
                return;
            }

            if (_student && k.Student != null)
                return;

            if (_allZaposleniks && k.Zaposlenik != null)
                return;

            List<Zaposlenje> zaposlenja = Autentifikacija.getZaposlenjes(filterContext.HttpContext);

            if (zaposlenja != null)
            {
                //provjera
                foreach (KorisnickaUloga x in _zaposlenikKorisnickaUlogas)
                {
                    if (zaposlenja.Any(z=>z.KorisnickaUloga == x))
                        return;
                }
            }

            //ako funkcija nije prekinuta pomoću "return", onda korisnik nema pravo pistupa pa će se vršiti redirekcija na "/Home/Index"
            filterContext.HttpContext.Response.Redirect("/Autentifikacija");
        }
    }
}