using eAkademija.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eAkademija.Data.Model
{
    public class Student
    {
        //public Student()
        //{
        //    StudentZnacka = new List<StudentZnacka>();
        //    StudentKurs = new List<StudentKurs>();
        //    StudentPitanje = new List<StudentPitanje>();
        //}

        public string Id { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual List<StudentZnacka> StudentZnacka { get; set; }
        public virtual List<StudentKurs> StudentKurs { get; set; }
    }
}