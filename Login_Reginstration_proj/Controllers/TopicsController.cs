﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_Reginstration_proj.DbOperation;
using Login_Reginstration_proj.Models;

namespace Login_Reginstration_proj.Controllers
{

    [Authorize]
    public class TopicsController : Controller
    {
        UserOperation userOperation = null;
        public TopicsController()
        {
            userOperation = new UserOperation();
        }

        // GET: Topics
        public ActionResult userTopics()
        {
            return View();
        }
        //public ActionResult update()
        //{
        //    //User obj1= userOperation.getUserDetails(id);

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult update(string id,User user)
        //{
        //    //User obj1= userOperation.getUserDetails(id);
        //    if (ModelState.IsValid)
        //    {
        //       bool check= userOperation.editDetails(id, user);
        //        return RedirectToAction("login", "Login");
        //    }
        //    return View();
        //}

        public ActionResult edit(string name)
        {
            User obj1 = userOperation.getUserDetails(name);
            return View(obj1);
        }

        [HttpPost]
        public ActionResult edit(string name, User userModel)
        {

            // faizal webapii
            ValuesController vc = new ValuesController();
            User u1= vc.Put(name, userModel);
            if (u1 != null)
            {
                return View("userTopics");
            }
            else
            {
                ModelState.AddModelError("", "cannot update data");
                return View();
            }

            //my code

            //string check = userOperation.edit(name, userModel);
            //if (check.Equals("updated"))
            //{
            //    using (var context = new LoginRegistrationEntities())
            //    {
            //        User u = context.Users.FirstOrDefault(x => x.userName.Equals(userModel.userName));
            //        if (u != null)
            //        {
            //            name = u.firstName + " " + u.lastName;
            //        }
            //        TempData["name"] = name;
            //    }
            //    ViewBag.info = "details updated";
            //}
            //else if (check.Equals("not updated"))
            //{
            //    ViewBag.info = "details not updated";
            //}
            //else if (check.Equals("Err_UQ_KEY"))
            //{
            //    ViewBag.info = "User name is already there ";
            //}
            //else if (check.Equals("Err_PK_KEY"))
            //{
            //    ViewBag.info = "User is already registered!";
            //}

            //return View("userTopics");
        }
    }
}