using eAkademija.Data.DAL;
using eAkademija.Web.ViewModels;
using eAkademija.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eAkademija.Data.Model;

namespace eAkademija.Web.Reports.Student
{
    public class MojiKurseviReport
    {



        public class ZaglavljeVM
        {
            public string ImePrezime { get; set; }


            public string Datum { get; set; }

        }

        public class MojiKursevi
        {

            public int Id { get; set; }
            public string KursNaziv { get; set; }

        }


        public static List<ZaglavljeVM> GetZaglavlje(string id)
        {
            MyContext _ctx = new MyContext();

            AppUser podaci = _ctx.AppUserDbSet.Where(x => x.Id == id).FirstOrDefault();


            ZaglavljeVM zaglavlje = new ZaglavljeVM
            {
                ImePrezime = podaci.Ime + " " + podaci.Prezime,
          
                Datum = DateTime.Now.ToShortDateString()
            };

            return new List<ZaglavljeVM> { zaglavlje };


        }

        public static List<MojiKursevi> GetTijelo(string id)
        {
            MyContext _ctx = new MyContext();

            List<MojiKursevi> Model = new List<MojiKursevi>();
            string studentId = id;

            List<StudentKurs> studentKursevi = _ctx.StudentKursDbSet.Where(x => x.StudentId == studentId && x.DaLiJePrijavljen == true).ToList();
            List<Kurs> mojiKursevi = new List<Kurs>();
            foreach (var sk in studentKursevi)
            {

                foreach (var k in _ctx.KursDbSet.Where(x => !x.Status.ToString().Equals("ARCHIVED")).OrderByDescending(x => x.DatumKreiranja).ToList())
                {

                    if (k.Id == sk.KursId)
                        mojiKursevi.Add(k);

                }


            }

            Model.AddRange(mojiKursevi.Select(x => new MojiKursevi
            {

                Id = x.Id,
                KursNaziv = x.Naziv,
          

            }));


            return Model;
        }


    }
}