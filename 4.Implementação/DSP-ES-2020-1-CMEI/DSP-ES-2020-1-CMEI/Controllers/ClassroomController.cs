using _2_Application;
using DSP_ES_2020_1_CMEI.AuthorizeCustom;
using DSP_ES_2020_1_CMEI.Enums;
using DSP_ES_2020_1_CMEI.Models;
using DSP_ES_2020_1_CMEI.Util;
using System;
using System.Web.Mvc;

namespace DSP_ES_2020_1_CMEI.Controllers
{
    public class ClassroomController : Controller
    {
        private StudentModel studentBusinessModel;
        private LoginModel loginBusinessModel;
        private ClassroomModel classroomModel;
        private ClassroomApplication appClassroom;
        private ClassroomModel classroomBusinessModel;


        [CustomAuthorize]
        [HttpGet]
        public ActionResult ListClassroom()
        {
            classroomBusinessModel = new ClassroomModel();
            classroomModel = new ClassroomModel();
            loginBusinessModel = new LoginModel();

            try
            {
                classroomModel.idLoginAccess = loginBusinessModel.GetIdLoginAccessWithEmail(User.Identity.Name);
                classroomModel.listClassroomModel = classroomBusinessModel.ListClassroom(classroomModel.idLoginAccess);

                return View(classroomModel);
            }
            catch (Exception ex)
            {
                ViewBag.MessageType = MessageType.Error;
                ViewBag.Message = ex.Message;

                return View();

                throw;
            }
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult CreateClassroom()
        {
            studentBusinessModel = new StudentModel();
            classroomModel = new ClassroomModel();
            loginBusinessModel = new LoginModel();

            try
            {
                classroomModel.idLoginAccess = loginBusinessModel.GetIdLoginAccessWithEmail(User.Identity.Name);
                ViewBag.listClassroomStudent = new SelectList(studentBusinessModel.ListStudent(classroomModel.idLoginAccess), "idStudent", "nameStudent");

                return View(classroomModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult CreateClassroom(ClassroomModel form)
        {
            studentBusinessModel = new StudentModel();
            classroomModel = new ClassroomModel();
            appClassroom = new ClassroomApplication();

            try
            {
                string msgReturn = appClassroom.InsertListClassroom(form);

                int number;
                bool result = int.TryParse(msgReturn, out number);

                if (result)
                {
                    ViewBag.MessageType = MessageType.Success;
                    ViewBag.Message = Message.SuccessInsertClassroom;
                }
                else
                {
                    ViewBag.MessageType = MessageType.Error;
                    ViewBag.Message = Message.ErrorUnknown;
                }

                return Json(new { msg = ViewBag.Message, type = ViewBag.MessageType }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult UpdateClassroom()
        {
            return View();
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult DeleteClassroom()
        {
            return View();
        }
    }
}