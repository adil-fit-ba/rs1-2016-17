using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Models
{
    public class PredmetIndexVM
    {
        public class PredmetInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public float Ects { get; set; }
            public int GodinaStudija { get; set; }
            public int NppId { get; set; }
        }

        public List<PredmetInfo> Predmeti { get; set; }
        public string FakultetNaziv { get; set; }
        public string SmjerNaziv { get; set; }
        public string NppNaziv { get; set; }
        public int NppId { get; set; }
    }
}