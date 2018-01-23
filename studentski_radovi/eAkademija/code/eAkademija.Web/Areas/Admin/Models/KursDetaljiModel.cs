using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Admin.Models
{
    public class KursDetaljiModel
    {
        public string Naziv { get; set; }
        public DateTime Datum { get; set; }
        public string Opis { get; set; }
        public int brojPolaznika { get; set; }
    }
}