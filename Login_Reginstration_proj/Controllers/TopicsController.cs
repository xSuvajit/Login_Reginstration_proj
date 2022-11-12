using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login_Reginstration_proj.Controllers
{
    [Authorize]
    public class TopicsController : Controller
    {
        // GET: Topics
        public ActionResult userTopics()
        {
            return View();
        }
    }
}