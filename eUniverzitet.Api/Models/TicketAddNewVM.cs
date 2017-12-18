using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUniverzitet.Api.Models
{
    public class TicketAddNewVM
    {
        public int StudiranjeId { get; set; }
        public string Naslov { get; set; }
        public int? IzvodjenjePredmetaId { get; set; }
        public int? KategorijaId { get; set; }
        public string Poruka { get; set; }

       
    }
}