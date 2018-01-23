using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class Grad
    {
        public int Id { get; set; }

        public string Naziv { get; set; }
        public string PostanskiBroj { get; set; }

        public int? DrzavaId { get; set; }
        public Drzava Drzava { get; set; }

        public virtual List<AppUser> AppUsers { get; set; } // virtual if we want lazy loading
    }
}
