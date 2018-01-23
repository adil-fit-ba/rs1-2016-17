using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eAkademija.Data.Model
{
    public class ZadacaStudentKurs
    {
        public int Id { get; set; }

        public DateTime? DatumNapisan { get; set; }
        public string Rjesenje { get; set; }
        public int Poeni { get; set; }

        public int ZadacaId { get; set; }
        public virtual Zadaca Zadaca { get; set; }
        
        public int StudentKursId { get; set; }
        public virtual StudentKurs StudentKurs { get; set; }

    }
}