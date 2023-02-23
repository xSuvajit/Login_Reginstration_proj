using Login_Reginstration_proj.DbOperation;
using Login_Reginstration_proj.Models;
using System;
using System.Linq;
using System.Web.Mvc;
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
            bool isLogin = userOperation.login(u1);
            if (isLogin)
            {
                FormsAuthentication.SetAuthCookie(u1.userName.ToString(), false);                
                using (var context = new LoginRegistrationEntities())
                {
                    User u = context.Users.FirstOrDefault(x => x.userName.Equals(u1.userName));
                    if (u != null)
                    {
                        Session["name"] = u.firstName + " " + u.lastName;
                        Session["CurrentUserName"] = u.userName;
                        Session["lastLogIn"] = u.LastLoginTime!=null?u.LastLoginTime:DateTime.Now;
                        u.LastLoginTime = DateTime.Now;
                        context.SaveChanges();
                    }                    
                }
                Session["info"] = "";
                return RedirectToAction("userTopics", "Topics");
            }
            else
            {
                Session["info"] = "password or username is invalid";
            }
            return View();
        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            Session["info"] = "Log out Successfull!";
            return RedirectToAction("login");
        }
    }
}