using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC.Areas.ModulStudentskaSluzba.Models
{
    public class StudentUrediVM
    {
      
        public int Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno polje")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno polje")]
        public string Prezime { get; set; }

        public string BrojIndeksa { get; set; }

        public string KorisnickoIme { get; set; }
         [Required(ErrorMessage = "Lozinka je obavezno polje")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Lozinka mora imati najmanje 5 a najviše 30 karaktera")]
        public string Lozinka { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DatumRodjenja { get; set; }

        public int SmjerId { get; set; }
        public List<SelectListItem> SmjerStavke { get; set; }
    }
}

