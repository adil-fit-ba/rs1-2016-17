using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class PitanjeOdgovor
    {
        public int Id { get; set; }

        public DateTime DatumPitanja { get; set; }
        public DateTime? DatumOdgovora { get; set; }

        public string Pitanje { get; set; }
        public string Odgovor { get; set; }

        public int StudentKursId { get; set; }
        public virtual StudentKurs StudentKurs { get; set; }

        public string InstruktorId { get; set; }
        public virtual Instruktor Instruktor { get; set; }
    }
}
