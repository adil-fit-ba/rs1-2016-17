using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eAkademija.Data.Model
{
    public class StudentKurs
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int? KursId { get; set; }
        public virtual Kurs Kurs { get; set; }

        public DateTime? DatumPocetak { get; set; }
        public DateTime? DatumKraj { get; set; }
        
        public int Ocjena { get; set; }
        
        public bool DaLiJeFavorit { get; set; }
        public bool DaLiJePrijavljen { get; set; }
    }
}