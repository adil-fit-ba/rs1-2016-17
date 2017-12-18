using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Api.Models
{
    public class NotifikacijaPregledVM
    {
        public NotifikacijaInfoVM LastNotifikacija { get; set; }
        public int CountNotifikacije { get; set; }

        public class NotifikacijaInfoVM
        {
            public string Naslov { get; set; }
            public string Opis { get; set; }
        }
    }
}