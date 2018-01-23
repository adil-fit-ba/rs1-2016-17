using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class Administrator
    {
        public Administrator()
        {
            KursArhiva = new List<KursArhiva>();
        }

        public string Id { get; set; }
        public AppUser AppUser { get; set; }

        public virtual List<KursArhiva> KursArhiva { get; set; }
    }
}
