using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eUniverzitet.Api.Models;
using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Api.Controllers
{
    public class NotifikacijaController : ApiController
    {
        private MojContext ctx = new MojContext();

        [HttpGet]
        public NotifikacijaPregledVM Pregled(int korisnikId)
        {
            IQueryable<Notifikacija> notifikacijas = ctx.Notifikacijas.Where(x=>x.KorisnikId == korisnikId && !x.IsRead);
            NotifikacijaPregledVM model = new NotifikacijaPregledVM
            {
               LastNotifikacija = notifikacijas
                .OrderByDescending(x=>x.Vrijeme)
                   .Select(x=>new NotifikacijaPregledVM.NotifikacijaInfoVM
                   {
                       Naslov = x.Naslov,
                       Opis = x.Opis
                   }).FirstOrDefault(),
               CountNotifikacije = notifikacijas.Count()
            };

            return model;
        }
    }
}
