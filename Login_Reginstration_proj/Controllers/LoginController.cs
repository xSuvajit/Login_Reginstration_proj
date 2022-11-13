using Login_Reginstration_proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_Reginstration_proj.DbOperation;
using System.Web.Security;

namespace Login_Reginstration_proj.Controllers
{
    public class LoginController : Controller
    {
        UserOperation userOperation = null;   
        // GET: Login
        public LoginController()
        {
            userOperation = new UserOperation();
        }
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(User u1)
        {
            string name = "";
            bool isLogin = userOperation.login(u1);
            if (isLogin)
            {
                FormsAuthentication.SetAuthCookie(u1.userName.ToString(), false);
                //ViewBag.info = "Logged in work";
                
                using (var context = new LoginRegistrationEntities())
                {
                    User u = context.Users.FirstOrDefault(x => x.userName.Equals(u1.userName));
                    if (u != null)
                    {
                        name = u.firstName + " " + u.lastName;

                    }
                    ViewBag.name = name;
                }
                //ViewBag.name = name;
                return RedirectToAction("userTopics", "Topics");
            }
            else
            {
                ViewBag.info = "password or username is invalid";
            }

            return View();
        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }

        

    }
}