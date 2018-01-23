using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class Drzava
    {
        public int DrzavaId { get; set; }

        public string Naziv { get; set; }

        public virtual List<Grad> Gradovi { get; set; }
    }
}
