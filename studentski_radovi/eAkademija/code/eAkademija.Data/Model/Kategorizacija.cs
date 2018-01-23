using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class Kategorizacija
    {
        public int Id { get; set; }

        public int KursId { get; set; }
        public virtual Kurs Kurs { get; set; }

        public int PotkategorijaId { get; set; }
        public virtual Potkategorija Potkategorija { get; set; }

    }
}
