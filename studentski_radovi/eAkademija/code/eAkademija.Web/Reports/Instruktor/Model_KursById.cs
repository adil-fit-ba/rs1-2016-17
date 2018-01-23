using eAkademija.Data.DAL;
using eAkademija.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Reports.Instruktor
{
    public class Model_KursById
    {
        public class ZaglavljeVM
        {
            public int KursId { get; set; }

            public string KursNaziv { get; set; }
            public DateTime KursDatum { get; set; }

            public int KursPolaznikCount { get; set; }
            public int KursPolaznikOcijenjenCount { get; set; }

            public int KursZadanaZadacaCount { get; set; }
            public int KursZadanaZadacaUradjenaCount { get; set; }

            public int KursPitanjaCount { get; set; }
            public int KursOdgovorCount { get; set; }

            public double? KursPolaznikOcjenaAvg { get; set; }
        }

        public class TijeloVM
        {
            public string KursPolaznikPrezimeIme { get; set; }
            public string KursPolaznikOcjena { get; set; }
            public DateTime? KursPolaznikDatumKraj { get; set; }
        }



        public static List<ZaglavljeVM> GetZaglavlje(int id)
        {
            MyContext _ctx = new MyContext();
            
            return _ctx.KursDbSet.Where(w => w.Id == id).Select(x => new ZaglavljeVM
            {
                KursId = x.Id,
                KursNaziv = x.Naziv,
                KursDatum = x.DatumKreiranja,

                KursPolaznikCount = _ctx.StudentKursDbSet.Where(w => w.Kurs.Id == id).Count(),
                KursPolaznikOcijenjenCount = _ctx.StudentKursDbSet.Where(w => w.Kurs.Id == id && w.Ocjena > 5).Count(),
                KursPolaznikOcjenaAvg = _ctx.StudentKursDbSet.Where(w => w.Kurs.Id == id && w.Ocjena > 5).Select(s => s.Ocjena).Average(),

                KursPitanjaCount = _ctx.PitanjeOdgovorDbSet.Where(w => w.StudentKurs.Kurs.Id == id).Count(),
                KursOdgovorCount = _ctx.PitanjeOdgovorDbSet.Where(w => w.StudentKurs.Kurs.Id == id && w.Odgovor.Length > 0).Count(),

                KursZadanaZadacaCount = _ctx.ZadacaDbSet.Where(w => w.Kurs.Id == id).Count(),
                KursZadanaZadacaUradjenaCount = _ctx.ZadacaStudentKursDbSet.Where(w => w.StudentKurs.Kurs.Id == id && w.Poeni > -1).Count()

            }).ToList();
        }

        public static List<TijeloVM> GetTijelo(int id)
        {
            MyContext _ctx = new MyContext();

            return _ctx.StudentKursDbSet.Where(w => w.Kurs.Id == id).Select(x => new TijeloVM
            {
                KursPolaznikPrezimeIme = x.Student.AppUser.Prezime + " " + x.Student.AppUser.Ime,
                KursPolaznikDatumKraj = x.DatumKraj.Value,
                KursPolaznikOcjena = (x.Ocjena == -1 ? "" : x.Ocjena.ToString())

            }).OrderBy(orderby => orderby.KursPolaznikPrezimeIme).ToList();
        }
    }
}