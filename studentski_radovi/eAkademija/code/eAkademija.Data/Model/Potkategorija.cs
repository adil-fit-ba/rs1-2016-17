using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class Potkategorija
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public int KategorijaId { get; set; }
        public virtual Kategorija Kategorija { get; set; }
    }
}
