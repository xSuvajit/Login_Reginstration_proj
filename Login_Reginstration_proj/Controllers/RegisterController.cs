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

        public ActionResult Add()
        {
            User user = vc.Get(9876);
            return View(user);
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
                    ViewBag.info = "Registration Successfull!!";
                }
                else if(status.Equals("notAdded"))
                {
                    ViewBag.info = "data not added";
                }
                else if (status.Equals("Err_UQ_KEY"))
                {
                    ViewBag.info = "UserName is not Availeble! Please provide a unique UserName!";
                }
                else if (status.Equals("Err_PK_KEY"))
                {
                    ViewBag.info = "User is already registered!";
                }
            }
            return View();
        }
    }
}