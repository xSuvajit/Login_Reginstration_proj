using Login_Reginstration_proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_Reginstration_proj.DbOperation;

namespace Login_Reginstration_proj.Controllers
{
    public class RegisterController : Controller
    {
        UserOperation userOperation = null;
        ValuesController vc = new ValuesController();
        // GET: Login
        public RegisterController()
        {
            userOperation = new UserOperation();
        }

        public ActionResult addUsers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addUsers(User u1)
        {
            if (ModelState.IsValid)
            {
                string status = userOperation.addUsers(u1);
                if (status.Equals("added"))
                {
                    ModelState.Clear();
                    Session["info"] = "Registration Successfull!!";
                }
                else if(status.Equals("notAdded"))
                {
                    Session["info"] = "Something went Wrong!! Please try again!!";
                }
                else if (status.Equals("Err_UQ_KEY"))
                {
                    Session["info"] = "UserName is not Availeble! Please provide a unique UserName!";
                }
                else if (status.Equals("Err_PK_KEY"))
                {
                    Session["info"] = "User is already registered!";
                }
            }
            return RedirectToAction("login", "Login");
        }
    }
}