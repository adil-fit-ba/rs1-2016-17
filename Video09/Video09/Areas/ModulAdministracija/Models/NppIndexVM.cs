using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Models
{
    public class NppIndexVM
    {
        public class NppInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public string AkademskaGodina { get; set; }
        }

        public List<NppInfo> NPPs { get; set; }
        public int? SmjerId { get; set; }
        public string FakultetNaziv { get; set; }
        public string SmjerNaziv { get; set; }
    }
}