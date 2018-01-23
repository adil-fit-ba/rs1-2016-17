using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Models
{
    public class PredajeUrediVM
    {
        public int Id { get; set; }
        public List<SelectListItem> NastavniciStavke { get; set; }
        public List<SelectListItem> AkademskeGodineStavke { get; set; }
        public int NastavnikId { get; set; }
        public int PredmetId { get; set; }
        public int AkademskaGodinaId { get; set; }
        public string PredmetNaziv { get; set; }
        public string SmjerNaziv { get; set; }
        public string FakultetNaziv { get; set; }
        public string NppNaziv { get; set; }
    }
}