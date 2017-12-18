using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using eUniverzitet.Data.DAL;
using eUniverzitet.Data.Models;

namespace eUniverzitet.Api.Controllers
{
    public class DemoWebApiController : ApiController
    {
        // GET: DemoMvc
        public List<Student> Get()
        {
           MojContext ctx = new MojContext();

            List<Student> students = ctx.Students.ToList();
            return students;
        }

        // GET: DemoMvc
        [HttpGet]
        public List<Student> Nesto()
        {
            MojContext ctx = new MojContext();

            List<Student> students = ctx.Students.ToList();
            return students;
        }
    }
}