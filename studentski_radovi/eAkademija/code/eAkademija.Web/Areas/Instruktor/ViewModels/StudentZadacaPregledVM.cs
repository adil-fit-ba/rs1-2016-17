using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class StudentZadacaPregledVM
    {
        public int StudentZadacaId { get; set; }

        public string StudentZadacaRjesenje { get; set; }
        public int StudentZadacaPoeni { get; set; }
    }
}