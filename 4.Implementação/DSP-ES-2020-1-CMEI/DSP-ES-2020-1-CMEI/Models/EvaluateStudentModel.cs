using _2_Application;
using _3_Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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

        public EvaluateStudentModel LoadDataEvaluateStudent(int idClassroom, int idStudent)
        {
            appEvaluateStudent = new EvaluateStudentApplication();
            EvaluateStudentModel evaluateStudentModel = new EvaluateStudentModel();
            evaluateStudentModel.ClassroomModel = new ClassroomModel();
            evaluateStudentModel.StudentModel = new StudentModel();
            evaluateStudentModel.listEvaluateStudentGrade = new List<EvaluateStudentGrade>();

            try
            {
                foreach (DataRow linha in appEvaluateStudent.QuerieEvaluateStudentGrade(idClassroom, idStudent).Rows)
                {
                    if (!linha["idClassroom"].ToString().Equals(""))
                    {
                        if (!linha["idEvaluateStudent"].ToString().Equals(""))
                        {
                            evaluateStudentModel.idEvaluateStudent = Convert.ToInt32(linha["idEvaluateStudent"]);
                        }

                        evaluateStudentModel.idClassroom = Convert.ToInt32(linha["idClassroom"]);
                        evaluateStudentModel.ClassroomModel.nameClassroom = Convert.ToString(linha["nameClassroom"]);
                        evaluateStudentModel.idStudent = Convert.ToInt32(linha["idStudent"]);
                        evaluateStudentModel.StudentModel.registrationNumber = Convert.ToString(linha["registrationNumber"]);
                        evaluateStudentModel.StudentModel.nameStudent = Convert.ToString(linha["nameStudent"]);
                    }
                    else
                    {
                        EvaluateStudentGrade obj = new EvaluateStudentGrade();
                        obj.TeachingPlan = new TeachingPlan();

                        obj.idTeachingPlan = Convert.ToInt32(linha["idTeachingPlan"]);
                        obj.TeachingPlan.activityDescription = Convert.ToString(linha["activityDescription"]);

                        if (!linha["grade"].ToString().Equals(""))
                        {
                            obj.grade = Convert.ToDecimal(linha["grade"]);
                        }

                        evaluateStudentModel.listEvaluateStudentGrade.Add(obj);
                    }
                }

                return evaluateStudentModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EvaluateStudentModel LoadDataEvaluateClassroom(int idClassroom)
        {
            appEvaluateStudent = new EvaluateStudentApplication();
            EvaluateStudentModel evaluateStudentModel = new EvaluateStudentModel();
            evaluateStudentModel.ClassroomModel = new ClassroomModel();
            evaluateStudentModel.listEvaluateStudentModel = new List<EvaluateStudentModel>();

            EvaluateStudentModel obj = new EvaluateStudentModel();

            try
            {
                foreach (DataRow linha in appEvaluateStudent.QuerieDataEvaluateStudentReport(idClassroom).Rows)
                {
                    if (!linha["idClassroom"].ToString().Equals(""))
                    {
                        evaluateStudentModel.idClassroom = Convert.ToInt32(linha["idClassroom"]);
                        evaluateStudentModel.ClassroomModel.nameClassroom = Convert.ToString(linha["nameClassroom"]);
                    }
                    else if (!linha["idStudent"].ToString().Equals(""))
                    {
                        if (!linha["registrationNumber"].ToString().Equals(""))
                        {
                            if (obj.listEvaluateStudentGrade != null && obj.listEvaluateStudentGrade.Count > 0)
                            {
                                evaluateStudentModel.listEvaluateStudentModel.Add(obj);
                            }

                            obj = new EvaluateStudentModel();
                            obj.StudentModel = new StudentModel();
                            obj.listEvaluateStudentGrade = new List<EvaluateStudentGrade>();

                            obj.idStudent = Convert.ToInt32(linha["idStudent"]);
                            obj.StudentModel.registrationNumber = Convert.ToString(linha["registrationNumber"]);
                            obj.StudentModel.nameStudent = Convert.ToString(linha["nameStudent"]);

                            if (!linha["countGrade"].ToString().Equals(""))
                            {
                                int countGrade = Convert.ToInt32(linha["countGrade"]);
                                int countTeachingPlan = Convert.ToInt32(linha["countTeachingPlan"]);

                                obj.percEvaluateModel = countGrade * 100 / countTeachingPlan;
                            }
                        }
                        else
                        {
                            EvaluateStudentGrade evaluateStudentGrade = new EvaluateStudentGrade();
                            evaluateStudentGrade.TeachingPlan = new TeachingPlan();

                            evaluateStudentGrade.idTeachingPlan = Convert.ToInt32(linha["idTeachingPlan"]);
                            evaluateStudentGrade.TeachingPlan.activityDescription = Convert.ToString(linha["activityDescription"]);

                            if (!linha["grade"].ToString().Equals(""))
                            {
                                evaluateStudentGrade.grade = Convert.ToDecimal(linha["grade"]);
                            }

                            obj.listEvaluateStudentGrade.Add(evaluateStudentGrade);
                        }
                    }
                }

                if (evaluateStudentModel.idClassroom > 0)
                {
                    evaluateStudentModel.listEvaluateStudentModel.Add(obj);
                }

                return evaluateStudentModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckConclusionClassroom(EvaluateStudentModel evaluateStudentModel)
        {
            bool conclusionClassroom = true;

            if (evaluateStudentModel.listEvaluateStudentModel != null && evaluateStudentModel.listEvaluateStudentModel.Count > 0)
            {
                foreach (var item in evaluateStudentModel.listEvaluateStudentModel)
                {
                    if (item.percEvaluateModel != 100)
                    {
                        conclusionClassroom = false;
                    }
                }
            }
            else
            {
                conclusionClassroom = false;
            }

            return conclusionClassroom;
        }

        public byte[] GenerateReportEvaluateStudent(EvaluateStudentModel evaluateStudentModel)
        {
            try
            {
                //Generate report in string
                string report = "";

                report += "[Classroom]";
                report += Environment.NewLine;

                report += string.Concat("cod=", evaluateStudentModel.idClassroom, Environment.NewLine);
                report += string.Concat("name=", evaluateStudentModel.ClassroomModel.nameClassroom, Environment.NewLine);

                report += Environment.NewLine;
                report += Environment.NewLine;

                int contStudent = 0;

                foreach (var item in evaluateStudentModel.listEvaluateStudentModel)
                {
                    contStudent++;

                    report += "[Student" + contStudent + "]";
                    report += Environment.NewLine;

                    report += string.Concat("registrationNumber=", item.StudentModel.registrationNumber, Environment.NewLine);
                    report += string.Concat("name=", item.StudentModel.nameStudent, Environment.NewLine);

                    report += Environment.NewLine;

                    int contGrade = 0;

                    foreach (var itemII in item.listEvaluateStudentGrade)
                    {
                        contGrade++;

                        report += "[Activity" + contGrade + "]";
                        report += Environment.NewLine;

                        report += string.Concat("activityDescription=", itemII.TeachingPlan.activityDescription, Environment.NewLine);
                        report += string.Concat("grade=", itemII.grade, Environment.NewLine);

                        report += Environment.NewLine;
                    }

                    report += Environment.NewLine;
                }

                byte[] arrayByteReport = Encoding.ASCII.GetBytes(report);

                return arrayByteReport;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}