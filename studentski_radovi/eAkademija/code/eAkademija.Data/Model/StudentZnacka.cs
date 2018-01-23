using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class StudentZnacka
    {
        [Key, Column(Order = 0)]
        public string StudentId { get; set; }
        [Key, Column(Order = 1)]
        public int ZnackaId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Znacka Znacka { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Datum { get; set; }
    }
}
