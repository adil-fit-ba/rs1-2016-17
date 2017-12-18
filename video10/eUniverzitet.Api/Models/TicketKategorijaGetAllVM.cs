using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUniverzitet.Api.Models
{
    public class TicketKategorijaGetAllVM
    {
        public class TicketKategorijaVM
        {
            public int KategorijaID { get; set; }
            public String Opis { get; set; }
        }

        public List<TicketKategorijaVM> kategorijaInfos { get; set; }
    }
}