using eAkademija.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class KursPregledVM
    {
        public int KursId { get; set; }

        public string KursNaziv { get; set; }
        public DateTime KursDatumKreiranja { get; set; }
        public string KursOpis { get; set; }
        public KursNivo KursNivo { get; set; }
        public string KursVideoId { get; set; }
        public KursStatus KursStatus { get; set; }

        public int KursOcjenaCount { get; set; }
        
        public Double? KursOcjenaAvg { get; set; }

        public int KursZadacaCount { get; set; }
        public int KursPitanjeOdgovorCount { get; set; }
    }
}