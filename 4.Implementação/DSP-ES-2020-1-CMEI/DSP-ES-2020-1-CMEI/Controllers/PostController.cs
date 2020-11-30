using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using _2_Application;
using _3_Domain;
using DSP_ES_2020_1_CMEI.AuthorizeCustom;
using DSP_ES_2020_1_CMEI.Enums;
using DSP_ES_2020_1_CMEI.Models;
using DSP_ES_2020_1_CMEI.Util;

namespace DSP_ES_2020_1_CMEI.Controllers
{
    public class PostController : Controller
    {
        private PostApplication postApplication = new PostApplication();

        [CustomAuthorize]
        [HttpGet]
        public ActionResult ListPost()
        {
            StudentsRoutineModel model = new StudentsRoutineModel();
            model.posts = postApplication.ListAllPosts();
            return View(model);
        }

        [HttpGet]
        public FileContentResult RenderPostImage(string picId)
        {
            var pic = postApplication.GetPostImage(picId);
            return new FileContentResult(pic, "mime/jpeg");
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult CreatePost()
        {
            PostModel postModel = new PostModel();
            ViewBag.cmeiList = new SelectList(postModel.FetchCmeis(), "id", "name");

            return View();
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult CreatePost(Post form, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    var target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    var data = target.ToArray();
                    form.imageId = postApplication.UpLoadImage(file.FileName, data);
                }

                postApplication.SavePost(form);
                ViewBag.MessageType = MessageType.Success;
                ViewBag.Message = Message.SuccessInsertPost;
                return RedirectToAction("ListPost");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ViewBag.MessageType = MessageType.Error;
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}