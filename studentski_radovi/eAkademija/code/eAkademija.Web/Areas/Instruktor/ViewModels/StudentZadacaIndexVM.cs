using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class StudentZadacaRow
    {
        public int StudentZadacaId { get; set; }

        public string StudentZadacaImeStudenta { get; set; }

        public DateTime? StudentZadacaDatumNapisan { get; set; }
        public string StudentZadacaRjesenje { get; set; }
        public int StudentZadacaPoeni { get; set; }
        public int StudentZadacaUkupnoBodova { get; set; }

        public int StudentZadacaKursId { get; set; }

    }

    public class StudentZadacaIndexVM
    {
        public List<StudentZadacaRow> StudentZadace { get; set; }
    }
}