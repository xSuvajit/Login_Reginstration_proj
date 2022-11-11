using Login_Reginstration_proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_Reginstration_proj.DbOperation
{
    public class UserOperation
    {
        public int addUsers(User userModel)
        {
            int usrCount = 0;
            using (var context=new LoginRegistrationEntities())
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
                usrCount++;
            }
            return (usrCount);
        }

    }
}