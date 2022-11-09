using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Login_Reginstration_proj.Models;

namespace Login_Reginstration_proj.Controllers
{
    
    public class ValuesController : ApiController
    {
       private HCLEntities obj = new HCLEntities ();
        // GET api/values
        public IEnumerable<Employee> Get()
        {
            return obj.Employees.ToList ();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
