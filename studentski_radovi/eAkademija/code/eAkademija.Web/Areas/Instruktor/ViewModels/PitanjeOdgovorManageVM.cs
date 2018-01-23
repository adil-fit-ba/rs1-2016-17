using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class PitanjeOdgovorManageVM
    {
        public int PitanjeOdgovorId { get; set; }

        [Required(ErrorMessage = "Odgovor ne smije biti prazan!")]
        public string PitanjeOdgovorOdgovor { get; set; }

        public int PitanjeOdgovorKursId { get; set; }
    }
}