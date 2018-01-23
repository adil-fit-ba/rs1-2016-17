using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAkademija.Data.Enums
{
    public enum KursStatus
    {
        [Display(Name = "Aktivan")]
        ACTIVE = 0,

        [Display(Name = "Označen za arhiviranje")]
        FLAGGED_FOR_ARCHIVING = 1,

        [Display(Name = "Arhiviran")]
        ARCHIVED = 2
    }

    public enum KategorijaStatus
    {
        [Display(Name = "Potrebno odobrenje za aktivaciju")]
        FLAGGED_FOR_PERMISSION = 0,

        [Display(Name = "Odobrena")]
        ACTIVE = 1,
    }
}
