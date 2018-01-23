using CondorExtreme3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CondorExtreme3_API.Controllers
{
    public class CitiesController : ApiController
    {
        public CondorDBXEntities principal = new CondorDBXEntities();

        [HttpGet]
        public object GetCities()
        {
            return principal.Cities.Where(x => x.IsDeleted == false).Select(x => new
            {
                CityID=x.CityID,
                Name=x.Name,
                PostalCode=x.PostalCode

            }).ToList();
        }
    }
}
