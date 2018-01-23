using eAkademija.Data.DAL;
using eAkademija.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Reports.Instruktor
{
    public class Model_KurseviByInstruktor
    {
        public class ZaglavljeVM
        {
            public string InstruktorId { get; set; }
            
            public string InstruktorImePrezime { get; set; }

        }

        public class TijeloVM
        {
            public int KursId { get; set; }

            public DateTime KursDatum { get; set; }
            public string KursNaziv { get; set; }

            public int KursPolazniciCount { get; set; }
        }



        public static List<ZaglavljeVM> GetZaglavlje(string instruktorId)
        {
            MyContext _ctx = new MyContext();

            return _ctx.KursDbSet.Where(w => w.Instruktor.Id == instruktorId).Select(x => new ZaglavljeVM
            {
                InstruktorId = x.Instruktor.Id,
                InstruktorImePrezime = x.Instruktor.AppUser.Ime + " " + x.Instruktor.AppUser.Prezime

            }).ToList();
        }

        public static List<TijeloVM> GetTijelo(string instruktorId)
        {
            MyContext _ctx = new MyContext();

            return _ctx.KursDbSet.Where(w => w.Instruktor.Id == instruktorId).Select(x => new TijeloVM
            {
                KursId = x.Id,
                KursDatum = x.DatumKreiranja,
                KursNaziv = x.Naziv,
                KursPolazniciCount = _ctx.StudentKursDbSet.Where(w => w.Kurs.Instruktor.Id.Equals(instruktorId) && w.Kurs.Id == x.Id).Count()
            }).ToList();
        }
    }
}