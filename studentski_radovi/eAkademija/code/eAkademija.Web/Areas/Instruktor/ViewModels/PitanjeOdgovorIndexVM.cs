using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class PitanjeOdgovorRow
    {
        public int PitanjeOdgId { get; set; }

        public string PitanjeOdgStudentImePrezime { get; set; }
        public string PitanjeOdgStudentSlikaPutanja { get; set; }

        public DateTime PitanjeOdgDatumPitanja { get; set; }
        public string PitanjeOdgPitanje { get; set; }

        public DateTime? PitanjeOdgDatumOdgovora { get; set; }
        public string PitanjeOdgOdgovor { get; set; }
    }

    public class PitanjeOdgovor
    {
        public List<PitanjeOdgovorRow> PitanjaOdgovori { get; set; }
    }
}