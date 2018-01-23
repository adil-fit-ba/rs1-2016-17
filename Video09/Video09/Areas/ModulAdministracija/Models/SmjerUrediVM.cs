using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Areas.ModulAdministracija.Models
{
    public class SmjerUrediVM
    {
        public int Id { get; set; }
        public string SmjerNaziv { get; set; }
        public int FakultetId { get; set; }
        public string FakultetNaziv { get; set; }
    }
}