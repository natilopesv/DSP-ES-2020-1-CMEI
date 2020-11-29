using DSP_ES_2020_1_CMEI.AuthorizeCustom;
using DSP_ES_2020_1_CMEI.Enums;
using DSP_ES_2020_1_CMEI.Models;
using DSP_ES_2020_1_CMEI.Util;
using RandomSolutions;
using System;
using System.IO;
using System.Web.Mvc;

namespace DSP_ES_2020_1_CMEI.Controllers
{
    public class StudentController : Controller
    {
        /* Global variables
       */

        private StudentModel studentBusinessModel;
        private StudentModel studentModel;
        private LoginModel loginBusinessModel;


        /* Views
 */

        [CustomAuthorize]
        [HttpGet]
        public ActionResult ListStudent()
        {
            studentBusinessModel = new StudentModel();
            studentModel = new StudentModel();
            loginBusinessModel = new LoginModel();

            try
            {
                studentModel.idLoginAccess = loginBusinessModel.GetIdLoginAccessWithEmail(User.Identity.Name);
                studentModel.listStudentModel = studentBusinessModel.ListStudent(studentModel.idLoginAccess);

                return View(studentModel);
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
        [HttpPost]
        public JsonResult ImportStudent(StudentModel form)
         {
            studentBusinessModel = new StudentModel();
            studentModel = new StudentModel();

            try
            {
                string msgReturn = studentBusinessModel.ImportStudent(form, Request.Files);

                if (msgReturn.Contains("Não é possível inserir uma linha de chave duplicada"))
                {
                    ViewBag.MessageType = MessageType.Error;
                    ViewBag.Message = Message.ErrorDuplicateStudent;
                }
                else
                {
                    int number;
                    bool result = int.TryParse(msgReturn, out number);

                    if (result)
                    {
                        ViewBag.MessageType = MessageType.Success;
                        ViewBag.Message = Message.SuccessImportStudent;
                    }
                    else
                    {
                        ViewBag.MessageType = MessageType.Error;
                        ViewBag.Message = Message.ErrorUnknown;
                    }
                }

                return Json(new { msg = ViewBag.Message, type = ViewBag.MessageType }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message, type = MessageType.Success }, JsonRequestBehavior.AllowGet);
            }
        }

        [CustomAuthorize]
        [HttpPost]
        public JsonResult ExportStudent(StudentModel form)
        {
            studentBusinessModel = new StudentModel();
            studentModel = new StudentModel();

            try
            {
                byte[] excel = studentBusinessModel.ExportStudent(form);
                string excelBase64 = Convert.ToBase64String(excel);

                return Json(new { msg = "Alunos exportados", type = MessageType.Success, excelBase64 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message, type = MessageType.Success }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}