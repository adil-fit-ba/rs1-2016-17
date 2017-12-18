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
    public class TicketController : ApiController
    {
        private MojContext ctx = new MojContext();

        [HttpGet]
        [HttpPost]
        public TicketPregledVM AddNew(TicketAddNewVM model)
        {
            Ticket ticket = new Ticket
            {
                IsZatvoren = false,
                IzvodjenjePredmetaId = model.IzvodjenjePredmetaId,
                TicketKategorijaId = model.Kategorija,
                Naslov = model.Naslov,
                StudiranjeId = model.StudiranjeId,

            };

            int KorisnikId = ctx.Studiranjes.Where(x => x.Id == model.StudiranjeId).Select(s => s.Student.KorisnikId).Single();

            TicketPoruka ticketPoruka = new TicketPoruka
            {
                KorisnikId = KorisnikId,
                Poruka = model.Poruka,
                Ticket = ticket,
                Vrijeme = DateTime.Now
            };

            ctx.Tickets.Add(ticket);
            ctx.TicketDetails.Add(ticketPoruka);

        
            ctx.SaveChanges();

            return Pregled(model.StudiranjeId);
        }

        [HttpGet]
        [HttpPost]
        public TicketPregledVM Pregled(int StudiranjeId)
        {
            TicketPregledVM model = new TicketPregledVM
            {
                TicketInfos = ctx.Tickets.Where(x => x.StudiranjeId == StudiranjeId)
                    .OrderByDescending(x => x.TicketPorukas.OrderByDescending(d => d.Vrijeme).FirstOrDefault().Vrijeme)
                    .Select(x => new TicketPregledVM.TicketInfo
                    {
                        TicketId = x.Id,
                        Naslov = x.Naslov,
                        Kategorija = x.TicketKategorija.Opis,
                        Predmet = x.IzvodjenjePredmeta.Predmet.Naziv,
                        IsClosed = x.IsZatvoren,
                        ZadnjaPorukaKorisnik = x.TicketPorukas.OrderByDescending(d => d.Vrijeme).FirstOrDefault().Korisnik.Prezime + " " + x.TicketPorukas.OrderByDescending(d => d.Vrijeme).FirstOrDefault().Korisnik.Ime,
                        ZadnjaPorukaVrijeme = x.TicketPorukas.OrderByDescending(d => d.Vrijeme).FirstOrDefault().Vrijeme,
                        ZadnjaPorukaSadrzaj = x.TicketPorukas.OrderByDescending(d => d.Vrijeme).FirstOrDefault().Poruka,

                    }).ToList()
            };
            return model;
        }

        [HttpGet]
        [HttpPost]
        public TicketDetaljiVM Odgovori(int korisnikId, int ticketId, string odgovorStr)
        {
            TicketPoruka poruka = new TicketPoruka
            {
                KorisnikId = korisnikId,
                TicketId = ticketId,
                Poruka = odgovorStr,
                Vrijeme = DateTime.Now
            };

            ctx.TicketDetails.Add(poruka);

            ctx.SaveChanges();

            return Detalji(ticketId);
        }

        
        [HttpGet]
        [HttpPost]
        public TicketDetaljiVM Detalji(int ticketId)
        {
            TicketDetaljiVM model = ctx.Tickets.Where(t=>t.Id == ticketId).Select(x=>new TicketDetaljiVM
            {
                TicketId = x.Id,
                Naslov = x.Naslov,
                Kategorija = x.TicketKategorija.Opis,
                Predmet = x.IzvodjenjePredmeta.Predmet.Naziv,
                IsClosed = x.IsZatvoren,

                TicketDetaljiInfos = ctx.TicketDetails.Where(d => d.TicketId == ticketId)
                    .OrderBy(d => d.Vrijeme)
                    .Select(d => new TicketDetaljiVM.TicketDetaljiInfo()
                    {
                        PorukaKorisnikIme = d.Korisnik.Prezime + " " + d.Korisnik.Ime,
                        PorukaKorisnikId = d.Korisnik.Id,
                        PorukaVrijeme = d.Vrijeme,
                        PorukaSadrzaj = d.Poruka,

                    }).ToList()
            }).Single();

            return model;
        }

        [HttpGet]
        [HttpPost]
        public TicketKategorijaGetAllVM KategorijaGetAll()
        {
            TicketKategorijaGetAllVM model = new TicketKategorijaGetAllVM
            {
                kategorijaInfos = ctx.TicketKategorijas.Select(x=>new TicketKategorijaGetAllVM.TicketKategorijaVM
                {
                    KategorijaID = x.Id,
                    Opis = x.Opis
                }).ToList()
            };
            return model;
        }
    }
}
