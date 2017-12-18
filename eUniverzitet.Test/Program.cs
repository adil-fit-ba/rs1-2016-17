using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUniverzitet.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = new Fakultet
            {
                NaucnaOblastId = 1,
                Naziv = "Fakultet astrofizike"
            };


            using (var db = new MojContext())
            {
                db.Fakultets.Add(f);
                db.SaveChanges();
            }
        }
    }
}
