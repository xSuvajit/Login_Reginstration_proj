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
                        if (!entity.userName.Equals(u.userName))
                        {
                            entity.userName = u.userName;
                        }
                        entity.SecretId = u.SecretId;
                        entity.modified = DateTime.Now;
                        entity.modifiedBy = u.userName;
                        entity.createdBy = u.userName;
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

        public void AddTopics(out List<int> ids, out List<string> topics)
        {
            ids = new List<int>();
            topics = new List<string>();
            int id;
            string topic;
            using (LoginRegistrationEntities db = new LoginRegistrationEntities())
            {
                foreach(var item in db.Topics.AsEnumerable())
                {
                    id = item.Id;
                    topic = item.MyTopics;
                    ids.Add(id);
                    topics.Add(topic);
                }
            }
        }

        public void SaveTopic(int id,string username)
        {
            using (LoginRegistrationEntities db = new LoginRegistrationEntities())
            {
                Topic tc = db.Topics.FirstOrDefault(t => t.Id == id);
                User usr = db.Users.FirstOrDefault(u => u.userName.Equals(username));                
            }            
        }
    }
}
