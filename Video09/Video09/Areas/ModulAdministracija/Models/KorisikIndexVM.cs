using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Models
{
    public class KorisikIndexVM
    {
        public class KorisnikInfo
        {
            public string FakultetNaziv { get; set; }
            public int BrojStudenata { get; set; }
            public int BrojSmjerova { get; set; }
            public int Id { get; set; }
        }

        public List<KorisnikInfo> Korisnici { get; set; }
    }
}