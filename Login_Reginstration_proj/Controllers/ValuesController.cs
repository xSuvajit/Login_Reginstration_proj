﻿using Login_Reginstration_proj.Models;
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
        //public HttpResponseMessage getUserDetails(string userName = "All")
        //{
        //    using (var context = new LoginRegistrationEntities())
        //    {
        //        switch (userName.ToLower())
        //        {
        //            case "faizahmef":
        //                return Request.CreateResponse
        //                     (HttpStatusCode.OK,
        //                     context.Users.Where
        //                     (s => s.userName.ToLower() == "faizahmef").
        //                     ToList());
        //            //            Select ( s => new User ()
        //            //{
        //            //    firstName = s.firstName,
        //            //    lastName = s.lastName,
        //            //    userName = s.userName,
        //            //    SecretId = s.SecretId
        //            //} ).FirstOrDefault ();
        //            //return user;
        //            default: return Request.CreateResponse(HttpStatusCode.BadRequest, "Not Valid");
        //        }
        //    }
        //}
        // POST api/values
        public void Post(User u)
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
