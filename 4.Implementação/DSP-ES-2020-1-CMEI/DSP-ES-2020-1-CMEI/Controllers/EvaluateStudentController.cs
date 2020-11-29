using DSP_ES_2020_1_CMEI.AuthorizeCustom;
using DSP_ES_2020_1_CMEI.Models;
using System;
using System.Web.Mvc;

namespace DSP_ES_2020_1_CMEI.Controllers
{
    public class EvaluateStudentController : Controller
    {
        private EvaluateStudentModel evaluateStudentModel;
        private EvaluateStudentModel evaluateStudentBusinessModel;
        private ClassroomModel classroomBusinessModel;
        private LoginModel loginBusinessModel;
        private StudentModel studentBusinessModel;

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
            studentBusinessModel = new StudentModel();

            try
            {
                evaluateStudentModel.idLoginAccess = loginBusinessModel.GetIdLoginAccessWithEmail(User.Identity.Name);
                evaluateStudentModel.listEvaluateStudentGrade = evaluateStudentBusinessModel.ListEvaluateStudentGrade(idClassroom, idStudent);

                //Search data student
                evaluateStudentModel.StudentModel = new StudentModel();
                evaluateStudentModel.StudentModel = studentBusinessModel.SearchStudent(idStudent);

                return View(evaluateStudentModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}