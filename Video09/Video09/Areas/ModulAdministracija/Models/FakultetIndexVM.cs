using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Models
{
    public class FakultetIndexVM
    {
        public class FakultetInfo
        {
            public string FakultetNaziv { get; set; }
            public int BrojStudenata { get; set; }
            public int BrojSmjerova { get; set; }
            public int Id { get; set; }
        }

        public List<FakultetInfo> Fakulteti { get; set; }
    }
}