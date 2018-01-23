using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Models
{
    public class NppUrediVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int SmjerId { get; set; }
        public int AkademskaGodinaId { get; set; }
        public List<SelectListItem> AkademskaGodinaStavke { get; set; }
        public string FakultetNaziv { get; set; }
        public string SmjerNaziv { get; set; }
    }
}