using eAkademija.Data.Enums;
using System;
using eAkademija.Data.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eAkademija.Web.ViewModels
{
    public class Front_Kurs
    {

        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime KursDatumKreiranja { get; set; }

        public string KursNaziv { get; set; }
        public string Instruktor { get; set; }
        public string KursKategorija { get; set; }
        public string KursOpis { get; set; }
        public string KursVideoId { get; set; }
        public string KursThumbImagePutanja { get; set; }
        public int? KursOcjena { get; set; }
        public float? StudentOcjena { get; set; }
        public int? StudentKursId { get; set; }
        public bool? JeUFavoritima { get; set; }
        public KursStatus KursStatus { get; set; }
        public KursNivo KursKursNivo { get; set; }
        public bool IsPrijavljenNaKurs { get; set; }
        public int KursPolazniciCount { get; set; }


        public List<Potkategorija> ListaPotKategorija { get; set; }
        public IEnumerable<SelectListItem> DropDownPotKategorija
        {

            get
            {

                List<SelectListItem> podaci = new List<SelectListItem>();
                podaci.Add(new SelectListItem { Value = "", Text = "Potkategorije" });

                podaci.AddRange(ListaPotKategorija.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }));

                return podaci;

            }


        }







    }
}