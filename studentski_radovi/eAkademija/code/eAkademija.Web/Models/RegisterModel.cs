using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eAkademija.Web.Models
{
    public class RegisterModel
    {   
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [EmailAddress (ErrorMessage ="Email mora biti u obliku email@email.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Tip { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [StringLength(100, ErrorMessage = "{0} mora imati bar {2} znakova.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }



        [Required]
        public int GradId { get; set; }

        public string ProfilePictureName { get; set; }



    }
}