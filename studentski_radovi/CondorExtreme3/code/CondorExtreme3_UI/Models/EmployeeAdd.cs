using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondorExtreme3_UI.Models
{
    public class EmployeeAdd
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CityBirthID { get; set; }
        public System.DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public string CurriculumVitae { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public bool IsDeleted { get; set; }
    }
}
