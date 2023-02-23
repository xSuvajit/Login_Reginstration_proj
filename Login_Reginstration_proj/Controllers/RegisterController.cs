using Login_Reginstration_proj.DbOperation;
using Login_Reginstration_proj.Models;
using System.Web.Mvc;

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
            Session["info"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult AddUsers(User u1)
        {
            if (ModelState.IsValid)
            {
                string status = userOperation.AddUsers(u1);
                if (status.Equals("added"))
                {
                    ModelState.Clear();
                    Session["info"] = "Registration Successfull!!";
                    return RedirectToAction("login", "Login");
                }
                else if(status.Equals("notAdded"))
                {
                    Session["info"] = "Something went Wrong!! Please try again!!";
                    return View();
                }
                else if (status.Equals("UNIQUE KEY"))
                {
                    Session["info"] = "UserName is not Availeble! Please provide a unique UserName!";
                    return View();
                }
                else if (status.Equals("PRIMARY KEY"))
                {
                    Session["info"] = "User is already registered!";
                    return RedirectToAction("login", "Login");
                }
                
            }
            return View();

        }
    }
}