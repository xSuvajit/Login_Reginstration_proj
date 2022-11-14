using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
            try
            {
                ValuesController vc = new ValuesController();
                User u1 = vc.Put(name, userModel);
                if (u1 != null)
                {
                    TempData["name"] = u1.firstName + " " + u1.lastName;
                    return View("userTopics");
                }
                else
                {
                   ViewBag.info= "cannot update data";
                    return View();
                }
            }
            catch (DbUpdateException ex)
            {
                string msg = ex.InnerException.InnerException.Message;
                if (msg.Contains("UNIQUE KEY"))
                {
                    ViewBag.info="UserName is not Availeble! Please provide a unique UserName!";
                    return View();
                }
                else
                {
                    ViewBag.info = "Some thing went wrong!";
                    return View();
                }
            }
            catch(DbEntityValidationException ex)
            {
                //ViewBag.info = "Field cant be blank! Please fill all the data!";
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

        [HttpPost]
        public ActionResult delete(string name)
        {
            ValuesController vc = new ValuesController();
            TempData["name"] = name;
            if (vc.Delete(name))
            {
                return View("login");
            }
            else 
            {
                ViewBag.info = "Not Deleted!";
                return View("userTopics");
            }
        }


    }
}