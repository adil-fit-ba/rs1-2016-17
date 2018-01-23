using System;
using MVC.Helper;

namespace MVC.Models
{
    public class UpisGodine:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? Datum4_LjetniOvjera { get; set; }
        public DateTime? Datum3_LjetniUpis { get; set; }
        public DateTime? Datum2_ZimskiOvjera { get; set; }
        public DateTime? Datum1_ZimskiUpis { get; set; }
        public int GodinaStudija { get; set; }


        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int AkademskaGodinaId { get; set; }
        public AkademskaGodina AkademskaGodina { get; set; }
        public float Cijena { get; set; }
    }
}
