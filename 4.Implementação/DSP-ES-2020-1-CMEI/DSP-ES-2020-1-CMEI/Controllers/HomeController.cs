using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2_Application;
using DSP_ES_2020_1_CMEI.Models;

namespace DSP_ES_2020_1_CMEI.Controllers
{
    public class HomeController : Controller
    {
        private PostApplication postApplication = new PostApplication();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentsRoutine()
        {
            StudentsRoutineModel model = new StudentsRoutineModel();
            model.posts = postApplication.ListAllPosts();
            return View(model);
        }
    }
}