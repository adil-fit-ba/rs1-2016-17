using eAkademija.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Models
{
    public class ShowViewModel
    {
        public ShowViewModel()
        {
            Kursevi = new List<Kurs>();
        }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Grad { get; set; }
        public string Tip { get; set; }
        public string ProfilePictureName { get; set; }
        public List<Kurs> Kursevi { get; set; }
    }
}