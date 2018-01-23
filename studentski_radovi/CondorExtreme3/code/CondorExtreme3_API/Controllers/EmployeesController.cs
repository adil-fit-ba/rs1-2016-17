using CondorExtreme3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Web.Http.Results;

namespace CondorExtreme3_API.Controllers
{
    public class EmployeesController : ApiController
    {
        public CondorDBXEntities principal= new CondorDBXEntities();

        [HttpGet]
        public object GetEmployees()
        {
            return principal.Employees.Where(x=>x.IsDeleted==false).Select(x => new
            {
                EmployeeID = x.EmployeeID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                City = x.Cities.Name,
                BirthDate = x.BirthDate,
                Gender = (x.Gender == false) ? "Male" : "Female",
                CurriculumVitae=x.CurriculumVitae,
                Email=x.Email,
                PhoneNumber=x.PhoneNumber,
                Username=x.Username

            }).ToList();
        }

        [Route("api/Employees/{EmployeeID}")]
        public object GetEmployees(int EmployeeID)
        {
            Employees E = principal.Employees.Where(x => x.EmployeeID == EmployeeID).FirstOrDefault();
            return new {
                EmployeeID = E.EmployeeID,
                FirstName = E.FirstName,
                LastName = E.LastName,
                City = E.Cities.Name,
                BirthDate = E.BirthDate,
                Gender = (E.Gender == false) ? "Male" : "Female",
                CurriculumVitae = E.CurriculumVitae,
                Email = E.Email,
                PhoneNumber = E.PhoneNumber,
                Username = E.Username
            };
        }

       
        public IHttpActionResult GetEmployees(string Username)
        {
            Employees Emp = principal.Employees.Where(x => x.Username == Username).FirstOrDefault();

            if (Emp == null)
                return NotFound();
            return Ok(Emp);
        }


        [HttpPost]
        public IHttpActionResult PostEmployees(dynamic obj)
        {
       
            if (!ModelState.IsValid)
                return BadRequest();


            Employees EMP = new Employees();

            EMP.FirstName = obj.FirstName;
            EMP.LastName = obj.LastName;
            EMP.CityBirthID = obj.CityBirthID;
            EMP.BirthDate = obj.BirthDate;
            EMP.Gender = obj.Gender;
            EMP.CurriculumVitae = obj.CurriculumVitae;
            EMP.Email = obj.Email;
            EMP.PhoneNumber = obj.PhoneNumber;
            EMP.Username = obj.Username;
            EMP.PasswordSalt= obj.PasswordSalt;
            EMP.PasswordHash= obj.PasswordHash;
            EMP.IsDeleted= obj.IsDeleted;

  
            principal.Employees.Add(EMP);
            principal.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id=EMP.EmployeeID },EMP);
        }

    }
}
