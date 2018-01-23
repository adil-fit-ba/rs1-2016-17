using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Areas.Admin.Models
{
    public class UserViewModel
    {   
        public string Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Grad { get; set; }
        public string Tip { get; set; }
    }
}