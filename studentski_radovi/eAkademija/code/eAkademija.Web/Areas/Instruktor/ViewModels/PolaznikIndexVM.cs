using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class PolaznikRow
    {
        public string PolaznikId { get; set; }

        public string PolaznikPrezimeIme { get; set; }

        public DateTime? PolaznikDatumPocetka { get; set; }
        public DateTime? PolaznikDatumZavrsetka { get; set; }

        public int PolaznikOcjena { get; set; }

        public string PolaznikSlikaPutanja { get; set; }
        public int PolaznikPohadjaKursCount { get; set; }

    }

    public class PolaznikIndexVM
    {
        public List<PolaznikRow> Polaznici { get; set; }

        public int PolaznikKursId { get; set; }
        public string PolaznikKursNaziv { get; set; }
    }
}