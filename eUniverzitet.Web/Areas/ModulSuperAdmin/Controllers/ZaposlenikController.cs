using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;
using eUniverzitet.Web.Areas.ModulSuperAdmin.Models;
using eUniverzitet.Web.Helper;

namespace eUniverzitet.Web.Areas.ModulSuperAdmin.Controllers
{
    [Autorizacija(student: false, zaposlenikKorisnickaUlogas: new[] {KorisnickaUloga.SuperAdministrator})]
    public class ZaposlenikController : Controller
    {

        private MojContext ctx = new MojContext();

        public ActionResult Index()
        {
            ZaposlenikIndexVM model = new ZaposlenikIndexVM
            {
                TabelaPodaci = ctx.Zaposleniks.Select(x => new ZaposlenikIndexVM.ZaposlenikInfo()
                {
                    Id = x.Id,
                    KorisnikId = x.KorisnikId,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    Zaposlenjas = x.Zaposlenja.Select(z => new ZaposlenikIndexVM.ZaposlenjeInfo
                    {
                        Id = z.Id,
                        OrganizacionaJedinica = z.OrganizacionaJedinica.Naziv,
                        RadnoMjesto = z.ZaposlenjeMjesto.Naziv,
                        DatumPocetak = z.UgovorPocetak,
                        KorisnickaUloga = z.KorisnickaUloga
                    }).ToList(),

                }).ToList()
            };


            return View(model);
        }


        public ActionResult Uredi(int zaposlenikId)
        {
            ZaposlenikUrediVM model =
                ctx.Zaposleniks.Where(x => x.Id == zaposlenikId).Select(x => new ZaposlenikUrediVM
                {
                    Id = x.Id,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    KorisnickoIme = x.Korisnik.KorisnickoIme,
                    Lozinka = x.Korisnik.Lozinka,

                }).Single();


            return View(model);
        }

        public ActionResult Dodaj(int korisnikId = 0)
        {
            ZaposlenikUrediVM model = new ZaposlenikUrediVM();
            model.korisnikId = korisnikId;

            return View("Uredi", model);
        }

        public ActionResult Obrisi(int zaposlenikId)
        {
            Zaposlenik x = ctx.Zaposleniks.Find(zaposlenikId);
            ctx.Zaposleniks.Remove(x);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Snimi(ZaposlenikUrediVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Uredi", vm);
            }

            Zaposlenik entity;
            if (vm.Id == 0)
            {
                entity = new Zaposlenik();
                if (vm.korisnikId == 0)
                {
                    entity.Korisnik = new Korisnik();
                    ctx.Zaposleniks.Add(entity);
                }
                else
                {
                    entity.Korisnik = ctx.Korisniks.Find(vm.korisnikId);
                }
            }
            else
            {
                entity = ctx.Zaposleniks
                    .Where(s => s.Id == vm.Id)
                    .Include(s => s.Korisnik)
                    .Single();
            }

            entity.Korisnik.Ime = vm.Ime;
            entity.Korisnik.Prezime = vm.Prezime;
            entity.Korisnik.KorisnickoIme = vm.KorisnickoIme;
            entity.Korisnik.Lozinka = vm.Lozinka;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}