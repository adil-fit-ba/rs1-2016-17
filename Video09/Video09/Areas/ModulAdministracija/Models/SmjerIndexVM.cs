using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Models
{
    public class SmjerIndexVM
    {
        public class SmjerInfo
        {
            public string SmjerNaziv { get; set; }
            public int BrojStudenata { get; set; }
            public int Id { get; set; }
        }

        public List<SmjerInfo> Smjerovi { get; set; }
        public int? FakultetId { get; set; }
        public string FakultetNaziv { get; set; }
    }
}