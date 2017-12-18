using System.Collections.Generic;
using System.Web.Mvc;

namespace eUniverzitet.Web.Areas.ModulAdministracija.Models
{
    public class PredmetUrediVM
    {
        public int Id { get; set; }
        public string PredmetNaziv { get; set; }


        public float Ects { get; set; }

        public int GodinaStudija { get; set; }
        public List<SelectListItem> GodinaStudijaStavke { get; set; }
        public int NppId { get; set; }
    }
}