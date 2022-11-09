using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Login_Reginstration_proj.Models;

namespace Login_Reginstration_proj.Controllers
{

    public class ValuesController : ApiController
    {
        private HCLEntities obj = new HCLEntities();

        //string cs = @"data source=STAR_DUST\SQLEXPRESS;initial catalog=HCL;integrated security=True";
        //string cons = getConString();

        private SqlConnection con = new SqlConnection(getConString());

        private static string getConString()
        {
            using (var streamReader = File.OpenText(@"C:\Connection.txt"))
            {
                var lines = streamReader.ReadToEnd();
                return lines;
            }
        }

        // GET api/values
        public IEnumerable<Employee> Get()
        {
            //using (SqlConnection con = new SqlConnection(cons))
            //{
            //    SqlCommand sql = new SqlCommand("select * from Employees", con);
            //    con.Open();
            //}

            con.Open();
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
