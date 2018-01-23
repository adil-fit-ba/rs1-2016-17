using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Areas.ModulStudentskaSluzba.Models.TokStudija;
using MVC.Models;

namespace MVC.Areas.ModulStudentskaSluzba.Models
{
    public class TokStudijaIndexVM
    {
        public OpsteInfoVM OpsteInfo { get; set; }
        public List<UpisaneGodineVM> UpisaneGodine { get; set; }
        public List<SlusaPredmeteVM> SlusaPredmete { get; set; }
        public List<UplataVM> Uplate { get; set; }
        public UpisGodineDetaljnoVM UpisGodineDetaljno { get; set; }
    }
}