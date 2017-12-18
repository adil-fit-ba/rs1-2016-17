using System.Collections.Generic;
using System.Web.Mvc;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Web.Areas.ModulSuperAdmin.Models
{
    public class OrganizacionaJedinicaUrediVM
    {
        public string Opis { get; set; }
        public int Id { get; set; }
        public OrganizacionaJedinicaTip Tip { get; set; }
        public IEnumerable<SelectListItem> TipStavke { get; set; }
        public int? NaucnaOblastId { get; set; }
        public IEnumerable<SelectListItem> NaucnaOblastStavke { get; set; }
    }
}