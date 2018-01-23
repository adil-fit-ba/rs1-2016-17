using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Model
{
    public class AppUser : IdentityUser
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public bool hasContentBan { get; set; }
        public string ProfilePictureName { get; set; }

        public virtual Instruktor Instruktor { get; set; }
        public virtual Student Student { get; set; }
        public virtual Administrator Administrator { get; set; }


        public int? GradId { get; set; } // grad not required (can change in future)
        public virtual Grad Grad { get; set; }

        public string Kind()
        {
            if(Instruktor != null)
            {
                return "Instruktor";
            }
            else if(Student != null)
            {
                return "Student";
            }
            else
            {
                return "Administrator";
            }
        }
    }
}
