using Login_Reginstration_proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login_Reginstration_proj.Controllers
{
    public class LoginController : Controller
    {
        ValuesController vc = new ValuesController();
        // GET: Login
        public ActionResult Add()
        {
            User user = vc.Get(1234567890);
            return View(user);
        }
    }
}