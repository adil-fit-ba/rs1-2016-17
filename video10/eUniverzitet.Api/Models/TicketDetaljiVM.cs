using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUniverzitet.Api.Models
{
    public class TicketDetaljiVM
    {
        public class TicketDetaljiInfo
        {
            public int TicketDetaljId { get; set; }
            public String PorukaKorisnikIme { get; set; }
            public int PorukaKorisnikId { get; set; }
            public DateTime PorukaVrijeme { get; set; }
            public String PorukaSadrzaj { get; set; }
        }

        public String Naslov { get; set; }
        public String Predmet { get; set; }
        public String Kategorija { get; set; }
        public bool IsClosed { get; set; }

        public int TicketId { get; set; }
        public List<TicketDetaljiInfo> TicketDetaljiInfos { get; set; }
    }

}