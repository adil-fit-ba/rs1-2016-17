using eAkademija.Data.DAL;
using eAkademija.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace eAkademija.Web.Reports.Administrator
{
    public class Model_Korisnici
    {
        public class ZaglavljeVM
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string KursText { get; set; }
            public string Tip { get; set; }
            public int brojKurseva { get; set; }
        }

        public class TijeloVM
        {
            public string KursIme { get; set; }
        }

        public static List<ZaglavljeVM> GetZaglavlje(string id)
        {
            MyContext _ctx = new MyContext();

            AppUser user = _ctx.AppUserDbSet
                .Include(x => x.Instruktor)
                .Include(x => x.Student)
                .Include(x => x.Student.StudentKurs)
                .Include(x => x.Instruktor.Kursevi)
                .Where(x => x.Id.Equals(id)).FirstOrDefault();
            
            ZaglavljeVM zaglavlje = new ZaglavljeVM
            {
                Ime = user.Ime,
                Prezime = user.Prezime
            };

            if(user.Student != null)
            {
                zaglavlje.Tip = "Student";
                zaglavlje.KursText = "Kursevi na koje ide ovaj student";
                zaglavlje.brojKurseva = user.Student.StudentKurs.Count();
            }else
            {
                zaglavlje.Tip = "Instruktor";
                zaglavlje.KursText = "Kursevi koje vodi ovaj instruktor";
                zaglavlje.brojKurseva = user.Instruktor.Kursevi.Count();
            }


            return new List<ZaglavljeVM> { zaglavlje };
        }

        public static List<TijeloVM> GetTijelo(string id)
        {
            MyContext _ctx = new MyContext();

            AppUser user = _ctx.AppUserDbSet
                .Include(x => x.Instruktor)
                .Include(x => x.Student)
                .Include(x => x.Student.StudentKurs)
                .Include(x => x.Instruktor.Kursevi)
                .Where(x => x.Id.Equals(id)).FirstOrDefault();


            if (user.Student != null)
            {
                List<TijeloVM> tijelo = user.Student.StudentKurs.Select(x => new TijeloVM
                {
                    KursIme = x.Kurs.Naziv
                }).ToList();

                return tijelo;
            }
            else
            {
                List<TijeloVM> tijelo = user.Instruktor.Kursevi.Select(x => new TijeloVM
                {
                    KursIme = x.Naziv
                }).ToList();

                return tijelo;
            }
            

        }

    
    }
}