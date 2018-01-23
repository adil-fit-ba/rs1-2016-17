using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAkademija.Data.Enums;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace eAkademija.Data.Model
{
    public class Kategorija
    {

        public Kategorija()
        {
            Podkategorije = new List<Potkategorija>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public KategorijaStatus KategorijaStatus { get; set;}
        public virtual List<Potkategorija> Podkategorije { get; set; }

        public string GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
        }
    }
}
