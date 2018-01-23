using eAkademija.Data.Enums;
using eAkademija.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class KursManageVM
    {
        public int KursId { get; set; }

        [Required(ErrorMessage = "Datum kursa je obavezan!")]
        public DateTime KursDatumKreiranja { get; set; }

        [Required(ErrorMessage = "Naziv kursa je obavezan!")]
        public string KursNaziv { get; set; }

        [Required(ErrorMessage = "Opis kursa je obavezan!")]
        public string KursOpis { get; set; }

        [Required(ErrorMessage = "Video ID kursa je obavezan!")]
        public string KursVideoId { get; set; }

        public KursStatus KursStatus { get; set; }

        [Required(ErrorMessage = "Nivo kursa je obavezan!")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public KursNivo? KursNivo { get; set; }

        public List<Kategorija> KursKategorijaList { get; set; }
        public List<Potkategorija> KursPotkategorijaList { get; set; }

        [Required(ErrorMessage = "Morate odabrati barem jednu kategoriju!")]
        public List<int> KursKategorizacija { get; set; }

    }
}