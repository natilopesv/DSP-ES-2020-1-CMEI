using _2_Application;
using _3_Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace DSP_ES_2020_1_CMEI.Models
{
    public class EvaluateStudentModel : EvaluateStudent
    {
        /* View attr
*/

        public decimal percEvaluateModel { get; set; }

        public ClassroomModel ClassroomModel { get; set; }

        public StudentModel StudentModel { get; set; }

        public List<EvaluateStudentModel> listEvaluateStudentModel { get; set; }


        /* Business rules

*/

        private EvaluateStudentApplication appEvaluateStudent;

        public List<EvaluateStudentModel> ListEvaluateStudent(int idClassroom)
        {
            appEvaluateStudent = new EvaluateStudentApplication();
            List<EvaluateStudentModel> listEvaluateStudentModel = new List<EvaluateStudentModel>();

            try
            {
                foreach (DataRow linha in appEvaluateStudent.QuerieEvaluateStudent(idClassroom).Rows)
                {
                    EvaluateStudentModel obj = new EvaluateStudentModel();
                    obj.ClassroomModel = new ClassroomModel();
                    obj.ClassroomModel.ClassroomStudent = new ClassroomStudent();
                    obj.ClassroomModel.ClassroomStudent.Student = new Student();

                    obj.idClassroom = Convert.ToInt32(linha["idClassroom"]);
                    obj.idStudent = Convert.ToInt32(linha["idStudent"]);
                    obj.ClassroomModel.nameClassroom = Convert.ToString(linha["nameClassroom"]);
                    obj.ClassroomModel.ClassroomStudent.Student.registrationNumber = Convert.ToString(linha["registrationNumber"]);
                    obj.ClassroomModel.ClassroomStudent.Student.nameStudent = Convert.ToString(linha["nameStudent"]);

                    if (!linha["countGrade"].ToString().Equals(""))
                    {
                        int countGrade = Convert.ToInt32(linha["countGrade"]);
                        int countTeachingPlan = Convert.ToInt32(linha["countTeachingPlan"]);

                        obj.percEvaluateModel = countGrade * 100 / countTeachingPlan;
                    }

                    listEvaluateStudentModel.Add(obj);
                }

                return listEvaluateStudentModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EvaluateStudentGrade> ListEvaluateStudentGrade(int idClassroom, int idStudent)
        {
            appEvaluateStudent = new EvaluateStudentApplication();
            List<EvaluateStudentGrade> listEvaluateStudentGrade = new List<EvaluateStudentGrade>();

            try
            {
                foreach (DataRow linha in appEvaluateStudent.QuerieEvaluateStudentGrade(idClassroom, idStudent).Rows)
                {
                    EvaluateStudentGrade obj = new EvaluateStudentGrade();
                    obj.TeachingPlan = new TeachingPlan();

                    if (!linha["idEvaluateStudent"].ToString().Equals(""))
                    {
                        obj.idEvaluateStudent = Convert.ToInt32(linha["idEvaluateStudent"]);
                    }

                    obj.idTeachingPlan = Convert.ToInt32(linha["idTeachingPlan"]);
                    obj.TeachingPlan.activityDescription = Convert.ToString(linha["activityDescription"]);

                    if (!linha["grade"].ToString().Equals(""))
                    {
                        obj.grade = Convert.ToDecimal(linha["grade"]);
                    }

                    listEvaluateStudentGrade.Add(obj);
                }

                return listEvaluateStudentGrade;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}