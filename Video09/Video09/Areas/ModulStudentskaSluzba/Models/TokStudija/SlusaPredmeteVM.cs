using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.ModulStudentskaSluzba.Models.TokStudija
{
    public class SlusaPredmeteVM
    {
        public float ECTS;
        public int GodinaStudija { get; set; }
        public string NazivPredmeta { get; set; }
        public int? Ocjena { get; set; }
        public DateTime? DatumOcjene { get; set; }
    }
}