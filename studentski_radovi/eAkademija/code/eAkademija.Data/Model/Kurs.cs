using eAkademija.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class Kurs
    {
        public int Id { get; set; }

        public string InstruktorId { get; set; }
        public virtual Instruktor Instruktor { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string VideoId { get; set; }

        public KursNivo Nivo { get; set; }
        public KursStatus Status { get; set; }

    }
}
