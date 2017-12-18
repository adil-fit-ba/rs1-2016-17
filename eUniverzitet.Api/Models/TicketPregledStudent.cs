using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Api.Models
{
    public class TicketPregledVM
    {
        public class TicketInfo
        {
            public int TicketId { get; set; }
            public string Naslov { get; set; }
            public string Predmet { get; set; }
            public string Kategorija { get; set; }
            public bool IsClosed { get; set; }

            public string ZadnjaPorukaKorisnik{ get; set; }
            public string ZadnjaPorukaSadrzaj { get; set; }

            public DateTime? ZadnjaPorukaVrijeme{ get; set; }

        }

         public List<TicketInfo> TicketInfos { get; set; }
    }
}