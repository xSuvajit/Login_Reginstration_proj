using Login_Reginstration_proj.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Login_Reginstration_proj.DbOperation
{
    public class UserOperation
    {
        public string addUsers(User userModel)
        {            
            try
            {
                using (var context = new LoginRegistrationEntities())
                {
                    User user = new User()
                    {
                        firstName = userModel.firstName,
                        lastName = userModel.lastName,
                        userName = userModel.userName,
                        SecretId = userModel.SecretId,
                        contact = userModel.contact,
                        createdBy = userModel.userName,
                        created = DateTime.Now,
                        modifiedBy = userModel.userName,
                        modified = DateTime.Now
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                    return "added";
                }
            }
            catch(DbUpdateException db)
            {                
                string msg = db.InnerException.InnerException.Message;
                if(msg.Contains("UNIQUE KEY"))
                {
                    return "Err_UQ_KEY";
                }
                if(msg.Contains("PRIMARY KEY"))
                {
                    return "Err_PK_KEY";
                }

            }
            return "notAdded";
        }

    }
}