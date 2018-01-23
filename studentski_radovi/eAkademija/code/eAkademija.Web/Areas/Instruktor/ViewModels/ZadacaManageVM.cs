using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Instruktor.ViewModels
{
    public class ZadacaManageVM
    {
        public int ZadacaId { get; set; }

        public int ZadacaKursId { get; set; }

        [Required(ErrorMessage = "Naslov Zadaće je obavezan!")]
        public string ZadacaNaslov { get; set; }

        [Required(ErrorMessage = "Opis Zadaće je obavezan!")]
        public string ZadacaOpis { get; set; }

        [Range(1, 100, ErrorMessage = "Unesite broj u rasponu od 1 do 100")]
        [Required(ErrorMessage = "Maksimalni broj bodova na Zadaći je obavezan!")]
        public int ZadacaUkupnoBodova { get; set; }

        [Required(ErrorMessage = "Unos roka za predaju Zadaće je obavezan!")]
        public DateTime ZadacaUraditiDo { get; set; }

    }
}