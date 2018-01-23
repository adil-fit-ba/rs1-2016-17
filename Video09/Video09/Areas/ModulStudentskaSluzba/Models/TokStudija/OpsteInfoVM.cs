using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.ModulStudentskaSluzba.Models.TokStudija
{
    public class OpsteInfoVM
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string OpstinaPrebivalista { get; set; }
        public string OpstinaRodjenja { get; set; }
        public string BrojIndeksa { get; set; }
        public string NPP { get; set; }
        public string Fakultet { get; set; }
        public string Smjer { get; set; }
        public int StudentId { get; set; }
    }
}