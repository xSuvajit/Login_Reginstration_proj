using Login_Reginstration_proj.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

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
        public User Put ([FromBody]string username,User u)
        {
            try
            {
                using (LoginRegistrationEntities db = new LoginRegistrationEntities())
                {
                    var entity = db.Users.FirstOrDefault(e => e.userName.Equals(username));
                    if (entity != null)
                    {
                        entity.firstName = u.firstName;
                        entity.lastName = u.lastName;
                        entity.SecretId = u.SecretId;
                        entity.modified = DateTime.Now;
                        db.SaveChanges();
                        return entity;
                    }
                    else
                        return null;
                }
            }
            catch (DbEntityValidationException)
            {
                throw;                
            }
        }

        // DELETE api/values/5
        public bool Delete([FromBody]string name)
        {
            using (LoginRegistrationEntities db = new LoginRegistrationEntities())
            {
                var entity = db.Users.FirstOrDefault(e => e.userName.Equals(name));
                if (entity != null)
                {
                    db.Users.Remove(entity);
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public SelectList AddTopics()
        {
            LoginRegistrationEntities db = new LoginRegistrationEntities();
            return new SelectList(db.Topics, "Id", "MyTopics");           
        }        
    }
}
