using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eAkademija.Web.Areas.Student.ViewModels
{
    public class PitanjeOdgovorRow
    {
        public int PitanjeOdgId { get; set; }

        public string PitanjeOdgStudentImePrezime { get; set; }
        public string PitanjeOdgStudentSlikaPutanja { get; set; }

        public string PitanjeOdgInstruktorImePrezime { get; set; }
        public string PitanjeOdgInstruktorSlikaPutanja { get; set; }

        public DateTime PitanjeOdgDatumPitanja { get; set; }
        public string PitanjeOdgPitanje { get; set; }

        public DateTime? PitanjeOdgDatumOdgovora { get; set; }
        public string PitanjeOdgOdgovor { get; set; }
    }

    public class PitanjeOdgovorVM
    {
        public List<PitanjeOdgovorRow> PitanjaOdgovori { get; set; }
    }

 
    public class PitanjeOdgovorPostavi {

        public int Id { get; set; }
        public DateTime DatumPitanja { get; set; }
        [AllowHtml][Required(ErrorMessage = "Popunite pitanje")]
        public string Pitanje { get; set; }

        public int StudentKursId { get; set; }
        public int KursId { get; set; }
    }
}