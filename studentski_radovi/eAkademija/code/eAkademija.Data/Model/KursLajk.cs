using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eAkademija.Data.Model
{
    public class KursLajk
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DatumLajkan { get; set; }

        public float Ocjena { get; set; }


        public int StudentKursId { get; set; }
        public virtual StudentKurs StudentKurs { get; set; }
    }
}
