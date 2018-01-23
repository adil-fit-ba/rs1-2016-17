using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Enums
{
    public enum KursNivo
    {
        [Display(Name="Početnik")]
        BEGINNER = 0,

        [Display(Name = "Srednji")]
        INTERMEDIATE = 1,

        [Display(Name = "Napredni")]
        ADVANCED = 2,

        [Display(Name = "Ekspert")]
        EXPERT = 3
    }
}
