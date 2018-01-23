using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class Zadaca
    {
        public int Id { get; set; }

        public string Naslov { get; set; }
        public string Opis { get; set; }
        public int UkupnoBodova { get; set; }
        public DateTime UraditiDo { get; set; }

        public int KursId { get; set; }
        public virtual Kurs Kurs { get; set; }
    }
}
