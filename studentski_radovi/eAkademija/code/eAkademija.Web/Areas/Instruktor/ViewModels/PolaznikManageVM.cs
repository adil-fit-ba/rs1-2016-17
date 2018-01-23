using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class PolaznikManageVM
    {
        public string PolaznikId { get; set; }
        public int PolaznikKursId { get; set; }

        [Range(5, 10, ErrorMessage = "Najniža ocjena na Kursu je 5 a najviša 10!")]
        public int PolaznikOcjena { get; set; }
    }
}