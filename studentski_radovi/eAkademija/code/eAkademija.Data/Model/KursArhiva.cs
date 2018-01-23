using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class KursArhiva
    {
        [Key, Column(Order = 0)]
        public string AdministratorId { get; set; }
        [Key, Column(Order = 1)]
        public int KursId { get; set; }

        public Administrator Administrator { get; set; }
        public Kurs Kurs { get; set; }

        public string Naziv { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DatumArhiviran { get; set; }
        public string Razlog { get; set; }
    }
}
