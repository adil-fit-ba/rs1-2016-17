using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class Znacka
    {
        public Znacka()
        {
            StudentZnacka = new List<StudentZnacka>();
        }

        public int ZnackaId { get; set; }
        public string Naziv { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Datum { get; set; }


        public virtual List<StudentZnacka> StudentZnacka { get; set; }
    }
}
