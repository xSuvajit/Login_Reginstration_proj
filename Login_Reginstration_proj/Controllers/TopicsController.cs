﻿using Login_Reginstration_proj.DbOperation;
using Login_Reginstration_proj.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

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

        public ActionResult edit(string name)
        {
            Session["info"] = "";
            User obj1 = userOperation.getUserDetails(name);
            if(obj1==null)
            {
                var context = new LoginRegistrationEntities();
                string usrname = Session["CurrentUserName"].ToString();
                User user = context.Users.FirstOrDefault(x => x.userName.Equals(usrname));
                obj1 = user;
                context.Dispose();
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
                    Session["info"] = "Details update succesffully!";
                    return RedirectToAction("userTopics");
                }
                else
                {
                   Session["info"]= "Cannot update data";
                    return View();
                }
            }
            catch (DbUpdateException ex)
            {
                string msg = ex.InnerException.InnerException.Message;
                if (msg.Contains("UNIQUE KEY"))
                {
                    Session["info"]="UserName is not Availeble! Please provide a unique UserName!";
                    return View();
                }
                else
                {
                    Session["info"] = "Something went wrong!";
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
                Session["info"] = "User deleted successfully!";
                return RedirectToAction("login", "Login");
            }
            else
            {
                Session["info"] = "Something went wrong!";
                return RedirectToAction("Topics");
            }
        }

        [HttpGet]
        public ActionResult addTopics()
        {
            ValuesController vc = new ValuesController();
            ViewBag.MyTopics = vc.AddTopics();
            Session["info"] = "";
            return View();           
        }

        public ActionResult EditStatusTopic(string topic)
        {
            Session["SelectedTopic"] = topic;
            Session["info"] = "";
            return View();
        }
 
        public ActionResult AfterEditStatusTopic()
        {
            string statusIdstr = Request.Form["TopicStatus"].ToString();
            int.TryParse(statusIdstr, out int StatusID);
            string currentUserName = Session["CurrentUserName"].ToString();
            string currentTopicName = Session["SelectedTopic"].ToString();
            if(userOperation.StatsUpdate(currentTopicName, currentUserName, StatusID))
            {
                Session["info"] = "Status updated successfully!";
                return RedirectToAction("userTopics", "Topics");
            }
            else
            {
                Session["info"] = "Something went wrong!";
                return RedirectToAction("userTopics", "Topics");
            }            
        }

        public ActionResult AddTopicToUserData()
        {
            ValuesController vc = new ValuesController();
            string data = Request.Form["MyTopics"].ToString();
            Int32.TryParse(data,out int selectedId);      
            if(selectedId==0)
            {
                Session["info"] = "Select one valid topic from the dropdown list!";
                return RedirectToAction("addTopics", "Topics");
            }
            string userName = Session["CurrentUserName"].ToString();
            if(!userOperation.IsTopicAlreadyAdded(userName,selectedId))
            {
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
                        Session["info"] = "Topic added successfully!";
                        return RedirectToAction("userTopics", "Topics");
                    }
                }
            }
            else
            {                
                Session["info"] = "Data already added!";
                return RedirectToAction("userTopics", "Topics");
            }
        }

        public ActionResult userTopics()
        {
            string usrname = Session["CurrentUserName"].ToString();
            ViewBag.UserTopics=userOperation.userSelectedTopics(usrname);
            
            return View();
        }

        public ActionResult ShowUserDetails()
        {
            string usrname = Session["CurrentUserName"].ToString();
            LoginRegistrationEntities context = new LoginRegistrationEntities();
            User user = context.Users.FirstOrDefault(x => x.userName.Equals(usrname));
            context.Dispose();
            return View(user);
        }

    }
}