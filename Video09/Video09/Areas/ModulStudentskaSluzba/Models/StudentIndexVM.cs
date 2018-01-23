using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC.Areas.ModulStudentskaSluzba.Models
{

    public class StudentIndexVM
    {
         public class StudentInfo
         {
             public string Fakultet_Naziv;
             public string Smjer_Naziv;
             public float EctsUkupno { get; set; }
             public int BrojPolozenihPredmeta { get; set; }
             public int Id { get; set; }
             public string Ime { get; set; }
             public string Prezime { get; set; }
             public string BrojIndeksa { get; set; }
         }

         public List<StudentInfo> studenti;

         public List<SelectListItem> smjeroviStavke { get; set; }
        public int SmjeriId { get; set; }
    }
}