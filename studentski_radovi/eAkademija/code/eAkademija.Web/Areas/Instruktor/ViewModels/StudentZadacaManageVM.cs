using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class StudentZadacaManageVM
    {
        public int StudentZadacaId { get; set; }
        public int StudentZadacaZadacaId { get; set; }

        [Range(-1, 100, ErrorMessage = "Unesite broj u rasponu od -1 (neocijenjeno) do 100 (max bodovi)")]
        [Required(ErrorMessage = "Ovo polje ne smije biti prazno!")]
        public int StudentZadacaPoeni { get; set; }

        public int StudentZadacaUkupnoBodova { get; set; }
    }
}