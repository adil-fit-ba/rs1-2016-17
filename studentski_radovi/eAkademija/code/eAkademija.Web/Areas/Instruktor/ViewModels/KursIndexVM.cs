using eAkademija.Data.Enums;
using eAkademija.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class KursRow
    {
        public int KursId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime KursDatumKreiranja { get; set; }

        public string KursNaziv { get; set; }
        public string KursOpis { get; set; }
        public string KursVideoId { get; set; }
        public string KursThumbImagePutanja { get; set; }
        
        public KursStatus KursStatus { get; set; }
        public KursNivo KursKursNivo { get; set; }

        public int KursPolazniciCount { get; set; }

    }


    public class KursIndexVM
    {
        public List<KursRow> Kursevi { get; set; }

        public string KursPretragaNaziv { get; set; }
        public string KursPretragaOpis { get; set; }

        public KursNivo? KursPretragaKursNivo { get; set; }
        public KursStatus? KursPretragaKursStatus { get; set; }

        public DateTime? KursPretragaDatumOd { get; set; }
        public DateTime? KursPretragaDatumDo { get; set; }

        public bool KursPretragaIzostaviBezPolaznika { get; set; }

    }
}