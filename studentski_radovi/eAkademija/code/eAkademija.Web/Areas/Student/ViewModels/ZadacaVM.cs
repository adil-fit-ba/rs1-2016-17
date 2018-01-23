using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eAkademija.Web.Areas.Student.ViewModels
{
    public class ZadacaVM
    {
        public int StudentZadacaId { get; set; }
        public DateTime DatumNapisan { get; set; }

        [AllowHtml]
        public string Rjesenje { get; set; }

        public int UkupnoBodova { get; set; }
        public int? TrenutnoBodova { get; set; }
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public DateTime UraditDo { get; set; }
        public int ZadacaId { get; set; }
        public int KursId { get; set; }
        public int StudentKursId { get; set; }

    }
}