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
            if(obj1==null)
            {
                var context = new LoginRegistrationEntities();
                string usrname = Session["CurrentUserName"].ToString();
                User user = context.Users.FirstOrDefault(x => x.userName.Equals(usrname));
                obj1 = user;                
            }
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
                    Session["CurrentUserName"] = u1.userName;
                    Session["name"] = u1.firstName + " " + u1.lastName;
                    return View("userTopics");
                }
                else
                {
                   ViewBag.info= "Cannot update data";
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

        public ActionResult delete()
        {
            string usrName = Session["CurrentUserName"].ToString();
            bool check=userOperation.deleteUser(usrName);
            if (check)
            {
                return RedirectToAction("login", "Login");
            }
            else
            {
                return RedirectToAction("Topics");
            }
        }

        [HttpGet]
        public ActionResult addTopics()
        {
            ValuesController vc = new ValuesController();
            ViewBag.MyTopics = vc.AddTopics();
            return View();           
        }



        public ActionResult EditStatusTopic()
        {            
            return View();
        }

 
        public ActionResult AfterEditStatusTopic()
        {
            string statusIdstr = Request.Form["TopicStatus"].ToString();
            int.TryParse(statusIdstr, out int StatusID);
            if (StatusID != 0)
            {

            }
            else
            {
                ViewBag.info = "Please select a status from dropdown list!";
            }
            return RedirectToAction("userTopics", "Topics");
        }

        public ActionResult Save()
        {
            ValuesController vc = new ValuesController();
            string data = Request.Form["MyTopics"].ToString();
            Int32.TryParse(data,out int selectedId);            
            string userName = Session["CurrentUserName"].ToString();
            using (LoginRegistrationEntities db = new LoginRegistrationEntities())
            {
                User u = db.Users.FirstOrDefault(x => x.userName.Equals(userName));
                Topic t = db.Topics.FirstOrDefault(x => x.Id == selectedId);
                if (t == null)
                {
                    return RedirectToAction("addTopics", "Topics");
                }
                else
                {
                    UserData userData = new UserData()
                    {
                        userName = u.userName,
                        MyTopics = t.MyTopics,
                        Status = t.Status,
                        created = DateTime.Now,
                        modified = DateTime.Now,
                        createdBy = u.userName,
                        modifiedBy = u.userName
                    };
                    db.UserDatas.Add(userData);
                    db.SaveChanges();
                    return RedirectToAction("userTopics", "Topics");
                }
            }
        }

        public ActionResult userTopics()
        {
            string usrname = Session["CurrentUserName"].ToString();
            ViewBag.UserTopics=userOperation.userSelectedTopics(usrname);
            return View();
        }



    }
}