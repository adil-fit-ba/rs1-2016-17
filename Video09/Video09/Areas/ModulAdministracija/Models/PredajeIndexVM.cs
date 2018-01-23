using System.Collections.Generic;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Models
{
    public class PredajeIndexVM
    {
        public class PredajeInfo
        {
            public int Id { get; set; }
            public string Nastavnik { get; set; }
            public string Predmet { get; set; }
            public string AkademskaGodina { get; set; }
        }
        public List<PredajeInfo> PredajeList { get; set; }
        public int PredmetId { get; set; }
        public string PredmetNaziv { get; set; }
        public string SmjerNaziv { get; set; }
        public string FakultetNaziv { get; set; }
        public string NppNaziv { get; set; }
    }
}