using DSP_ES_2020_1_CMEI.AuthorizeCustom;
using System.Web.Mvc;

namespace DSP_ES_2020_1_CMEI.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentsRoutine()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}