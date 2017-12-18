using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Api.Controllers
{
  

    public class PredmetiController : ApiController
    {
        private MojContext ctx = new MojContext();

        public class PredmetInfo
        {
            public string naziv { get; set; }
            public int? id { get; set; }
            public int? ocjena { get; set; }
            public string nastavnik { get; set; }
            public DateTime? datum { get; set; }
        }

        public class VM
        {
            public List<PredmetInfo> predmetiLista { get; set; }

        }

        [HttpGet]
        public VM GetAll(int studiranjeId)
        {
            return new VM
            {
                predmetiLista = ctx.SlusaPredmets
                    .Select(s => new PredmetInfo
                    {
                        naziv = s.IzvodjenjePredmeta.Predmet.Naziv,
                        datum = s.DatumOcjene,
                        ocjena = s.FinalnaOcjena,
                    }).ToList()
            };
        }
    }
}
