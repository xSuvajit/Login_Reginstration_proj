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
                    ViewBag.info = "Something went wrong!";
                    return View();
                }
            }
            catch(DbEntityValidationException)
            {
                return View();
            }
        }

        [HttpDelete]
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

        [HttpGet]
        public ActionResult addTopics()
        {
            ValuesController vc = new ValuesController();
            List<int> ids = new List<int>();
            List<string> topics = new List<string>();
            vc.AddTopics(out ids, out topics);
            ViewBag.ids = ids;
            ViewBag.topics = topics;
            return View();           
        }
    }
}