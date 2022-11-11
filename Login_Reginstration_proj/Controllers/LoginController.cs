using Login_Reginstration_proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_Reginstration_proj.DbOperation;

namespace Login_Reginstration_proj.Controllers
{
    public class LoginController : Controller
    {
        UserOperation userOperation = null;
        ValuesController vc = new ValuesController();
        // GET: Login
        public LoginController()
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
                int id = userOperation.addUsers(u1);
                if (id >= 1)
                {
                    ModelState.Clear();
                    ViewBag.info = "data added";
                }
                else
                {
                    ViewBag.info = "data not added";
                }
            }
            return View();
        }
    }
}