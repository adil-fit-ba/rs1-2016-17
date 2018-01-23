using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eAkademija.Data.Enums;

namespace eAkademija.Web.Areas.Admin.Models
{
    public class KursViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public KursStatus Status { get; set; }
        public string StatusIspis
        {
            get
            {
               return Status.ToString("D");
            }
        }
        
    }
}