using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class KursBiljeske
    {
        public int Id { get; set; }

        public string Biljeska { get; set; }
        public DateTime DatumKreiran { get; set; }

        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
    }
}
