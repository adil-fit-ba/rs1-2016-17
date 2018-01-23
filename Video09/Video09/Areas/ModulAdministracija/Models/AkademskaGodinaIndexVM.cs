using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Models
{
    public class AkademskaGodinaIndexVM
    {
        public class AkademskaGodinaInfo
        {
            public string Opis { get; set; }
            public int Id { get; set; }
        }

        public List<AkademskaGodinaInfo> AkademskeGodine { get; set; }
    }
}