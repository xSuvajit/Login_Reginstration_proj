using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login_Reginstration_proj.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "suvajit faizal Page";
            ViewBag.Title = "suvajit faizal koushik Page";
            ViewBag.Ayush = "Ayush Susmithha Page";
            return View();
        }
    }
}
