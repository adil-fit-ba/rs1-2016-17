using System;
using System.Collections.Generic;
using System.Linq;
using eUniverzitet.Data.DAL;

namespace eUniverzitet.Web.Reports
{
    public class Report1_Model
    {
        public class Body
        {
            public int BrojPolozenihPredmeta { get; set; }
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string BrojIndeksa { get; set; }
        }

        public class Header
        {
            public string Fakultet { get; set; }
            public string Odsjek { get; set; }
            public DateTime Datum { get; set; }
        }



        public static List<Body> GetBody(int odsjekId)
        {
            MojContext ctx = new MojContext();

            return ctx.Studiranjes
                .Where(x => x.Npp.OdsjekId == odsjekId)
                .Select(x => new Body
                {
                    BrojIndeksa = x.BrojIndeksa,
                    Id = x.Id,
                    Ime = x.Student.Korisnik.Ime,
                    Prezime = x.Student.Korisnik.Prezime,
                   
                    BrojPolozenihPredmeta =
                        ctx.SlusaPredmets.Count(y => y.UpisGodine.Studiranje.StudentId == x.Id && y.UpisGodine.Studiranje.Npp.OdsjekId == odsjekId && y.FinalnaOcjena != null)
                })
                .ToList();
        }

        public static List<Header> GetHeader(int odsjekId)
        {
            MojContext ctx = new MojContext();

            Header header = ctx.Odsjeks.Where(x=>x.Id == odsjekId).Select(x=>new Header
            {
                Fakultet = x.Fakultet.Naziv,
                Odsjek = x.Naziv,
                Datum = DateTime.UtcNow
            }).Single();

            return new List<Header>{header};
        }
    }
}