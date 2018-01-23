using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Helper;

namespace MVC.Models
{
    public class PrijavaIspita: IEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int SlusaPredmetId { get; set; }
        public SlusaPredmet SlusaPredmet { get; set; }


        public DateTime PrijavaDatum { get; set; }
        public int PrijavaKorisnikId { get; set; }
        public Korisnik PrijavaKorisnik { get; set; }


        public DateTime? OdjavaDatum { get; set; }
        public int? OdjavaKorisnikId { get; set; }
        public Korisnik OdjavaKorisnik { get; set; }
    }
}