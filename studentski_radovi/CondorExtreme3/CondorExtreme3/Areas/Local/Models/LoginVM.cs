using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CondorExtreme3.Areas.Local.Models
{
    public class LoginVM
    {
        [MaxLength(30, ErrorMessage = "Username is too long!")]
        [Required(ErrorMessage = "Username is required!")]
        public string UserName { get; set; }

        [MaxLength(30, ErrorMessage = "Password is too long!")]
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; } = "";
    }
}