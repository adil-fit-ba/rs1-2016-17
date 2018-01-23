using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class Instruktor
    {
        public string Id { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual List<Kurs> Kursevi { get; set; }
    }
}
