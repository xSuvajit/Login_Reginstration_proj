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

        public bool login(User user)
        {
            bool isLogin = false;          
            using(var context = new LoginRegistrationEntities())
            {
                bool isValidUser = context.Users.Any(x => x.userName == user.userName && x.SecretId == user.SecretId);
                if (isValidUser)
                {
                    isLogin = true;
                    return isLogin;
                }
                else
                {
                    isLogin = false;
                    return isLogin;
                }
            }            
        }

        public User getUserDetails(string userName)
        {
            using (var context = new LoginRegistrationEntities())
            {
                User user = context.Users.FirstOrDefault(s => s.userName.Equals(userName));
                return user;
            }
        }

        public string edit(string userName,User user)
        {
            try
            {
                using (var context = new LoginRegistrationEntities())
                {
                    var result = context.Users.FirstOrDefault(x => x.userName.Equals(userName));
                    if (result != null)
                    {
                        result.firstName = user.firstName;
                        result.lastName = user.lastName;                   
                        if (!user.userName.Equals(result.userName))
                        {
                            result.userName = user.userName;
                        } 
                        result.SecretId = user.SecretId;
                        result.modifiedBy = result.userName;
                        result.modified = DateTime.Now;
                        context.SaveChanges();
                        return "updated";
                    }                    
                }
            }
            catch(DbUpdateException ex)
            {
                string msg = ex.InnerException.InnerException.Message;
                if (msg.Contains("UNIQUE KEY"))
                {
                    return "Err_UQ_KEY";
                }
                
            }
            return "not updated";
        }

        public bool deleteUser(string username)
        {
            using (var context = new LoginRegistrationEntities())
            {
                var result = context.Users.FirstOrDefault(x => x.userName.Equals(username));
                if (result != null)
                {
                    context.Users.Remove(result);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

    }
}