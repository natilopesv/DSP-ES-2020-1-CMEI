using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSP_ES_2020_1_CMEI.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
    }
}