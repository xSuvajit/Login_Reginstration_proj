using Login_Reginstration_proj.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Login_Reginstration_proj.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "Suvajit is Great #Leader#OurBigBrother" };
        }

        // GET api/values/5
        public User Get(int id)
        {
            using (LoginRegistrationEntities db = new LoginRegistrationEntities())
            {
                return db.Users.Find(id);
            }
               
        }

        // POST api/values
        public void Post(User u)
        {
            using(LoginRegistrationEntities db = new LoginRegistrationEntities())
            {
                //if(db.Users.Find(u.contact)==null)
                //{
                    
                //    Console.WriteLine("test");
                //}
                db.Users.Add(u);
                db.SaveChanges();
                    //db.Users.Add(u);
                //if (db.Users.FirstOrDefault(e=>e.contact==u.contact) == null)
                    
            }
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
