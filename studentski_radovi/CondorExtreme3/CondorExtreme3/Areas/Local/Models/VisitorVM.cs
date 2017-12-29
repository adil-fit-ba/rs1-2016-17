using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CondorExtreme3.Areas.Local.Models
{
    public class VisitorVM
    {
        [Key]
        public int VisitorID { get; set; }

        [Required(ErrorMessage="Enter the phone number"),MaxLength(50, ErrorMessage = "Phone number too long!")]
        public string PhoneNumber { get; set; }

        public string ActivationCode { get; set; }
        public string ActivationConfirmationCode { get; set; }
        public string ErrorMessage { get; set; } = "";
        public string ProjectionId { get; set; }
        public List<string> Seats { get; set; }
    }
}