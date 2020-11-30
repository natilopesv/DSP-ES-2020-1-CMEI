using _2_Application;
using DSP_ES_2020_1_CMEI.AuthorizeCustom;
using DSP_ES_2020_1_CMEI.Enums;
using DSP_ES_2020_1_CMEI.Models;
using DSP_ES_2020_1_CMEI.Util;
using System;
using System.Web.Mvc;

namespace DSP_ES_2020_1_CMEI.Controllers
{
    public class EvaluateStudentController : Controller
    {
        private EvaluateStudentModel evaluateStudentModel;
        private EvaluateStudentModel evaluateStudentBusinessModel;
        private EvaluateStudentApplication appEvaluateStudent;
        private ClassroomModel classroomBusinessModel;
        private LoginModel loginBusinessModel;

        [CustomAuthorize]
        [HttpGet]
        public ActionResult ListEvaluateStudent()
        {
            evaluateStudentModel = new EvaluateStudentModel();
            evaluateStudentBusinessModel = new EvaluateStudentModel();
            classroomBusinessModel = new ClassroomModel();
            loginBusinessModel = new LoginModel();

            try
            {
                evaluateStudentModel.idLoginAccess = loginBusinessModel.GetIdLoginAccessWithEmail(User.Identity.Name);
                ViewBag.listClassroom = new SelectList(classroomBusinessModel.ListClassroom(evaluateStudentModel.idLoginAccess), "idClassroom", "nameClassroom");

                return View(evaluateStudentModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult ListEvaluateStudent(EvaluateStudentModel form)
        {
            evaluateStudentModel = new EvaluateStudentModel();
            evaluateStudentBusinessModel = new EvaluateStudentModel();
            classroomBusinessModel = new ClassroomModel();
            loginBusinessModel = new LoginModel();

            try
            {
                evaluateStudentModel.idLoginAccess = loginBusinessModel.GetIdLoginAccessWithEmail(User.Identity.Name);
                ViewBag.listClassroom = new SelectList(classroomBusinessModel.ListClassroom(evaluateStudentModel.idLoginAccess), "idClassroom", "nameClassroom");

                evaluateStudentModel.listEvaluateStudentModel = evaluateStudentBusinessModel.ListEvaluateStudent(form.idClassroom);

                return View(evaluateStudentModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult ApplyEvaluation(int idStudent, int idClassroom)
        {
            evaluateStudentModel = new EvaluateStudentModel();
            evaluateStudentBusinessModel = new EvaluateStudentModel();
            loginBusinessModel = new LoginModel();

            try
            {
                evaluateStudentModel = evaluateStudentBusinessModel.LoadDataEvaluateStudent(idClassroom, idStudent);
                evaluateStudentModel.idLoginAccess = loginBusinessModel.GetIdLoginAccessWithEmail(User.Identity.Name);

                return View(evaluateStudentModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult ApplyEvaluation(EvaluateStudentModel form)
        {
            string msgReturn = "";

            appEvaluateStudent = new EvaluateStudentApplication();

            try
            {
                form.evaluationDate = DateTime.Now;

                //First Evaluation 
                if (form.idEvaluateStudent == 0)
                {
                    msgReturn = appEvaluateStudent.InsertEvaluateStudent(form);
                }
                //More Evaluation
                else
                {
                    msgReturn = appEvaluateStudent.UpdateEvaluateStudent(form);
                }

                int number;
                bool result = int.TryParse(msgReturn, out number);

                if (result)
                {
                    ViewBag.MessageType = MessageType.Success;
                    ViewBag.Message = Message.SuccessEvaluate;
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
        public ActionResult ReadEvaluation(int idStudent, int idClassroom)
        {
            evaluateStudentModel = new EvaluateStudentModel();
            evaluateStudentBusinessModel = new EvaluateStudentModel();
            loginBusinessModel = new LoginModel();

            try
            {
                evaluateStudentModel = evaluateStudentBusinessModel.LoadDataEvaluateStudent(idClassroom, idStudent);

                return View(evaluateStudentModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult GenerateReportEvaluate(int? idClassroom)
        {
            evaluateStudentModel = new EvaluateStudentModel();
            evaluateStudentBusinessModel = new EvaluateStudentModel();
            loginBusinessModel = new LoginModel();

            try
            {
                if (idClassroom == null)
                {
                    ViewBag.MessageType = MessageType.Warning;
                    ViewBag.Message = Message.WarningSelectClassroom;

                    return Json(new { msg = ViewBag.Message, type = ViewBag.MessageType }, JsonRequestBehavior.AllowGet);
                }

                evaluateStudentModel = evaluateStudentBusinessModel.LoadDataEvaluateClassroom((int)idClassroom);

                if (!evaluateStudentBusinessModel.CheckConclusionClassroom(evaluateStudentModel))
                {
                    ViewBag.MessageType = MessageType.Warning;
                    ViewBag.Message = Message.WarningEvaluateIncompleteReport;

                    return Json(new { msg = ViewBag.Message, type = ViewBag.MessageType }, JsonRequestBehavior.AllowGet);
                }

                byte[] arrayByteReport = evaluateStudentBusinessModel.GenerateReportEvaluateStudent(evaluateStudentModel);
                string reportBase64 = Convert.ToBase64String(arrayByteReport);

                return Json(new { msg = "Relatório gerado", type = MessageType.Success, reportBase64 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}