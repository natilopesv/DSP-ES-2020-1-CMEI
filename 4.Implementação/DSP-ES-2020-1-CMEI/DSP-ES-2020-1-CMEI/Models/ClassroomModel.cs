using _2_Application;
using _3_Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace DSP_ES_2020_1_CMEI.Models
{
    public class ClassroomModel: Classroom
    {
        /* View attr
*/

        public int? idStudentModel { get; set; }

        public int totalStudentsModel { get; set; }

        public List<ClassroomModel> listClassroomModel { get; set; }


        /* Business rules

*/

        private ClassroomApplication appClassroom;

        public List<ClassroomModel> ListClassroom(int idLoginAccess)
        {
            appClassroom = new ClassroomApplication();
            List<ClassroomModel> listClassroom = new List<ClassroomModel>();

            try
            {
                foreach (DataRow linha in appClassroom.QuerieAllClassroom(idLoginAccess).Rows)
                {
                    ClassroomModel obj = new ClassroomModel();

                    obj.idClassroom = Convert.ToInt32(linha["idClassroom"]);
                    obj.nameClassroom = Convert.ToString(linha["nameClassroom"]);
                    obj.shiftClassroom = Convert.ToString(linha["shiftClassroom"]);
                    obj.totalStudentsModel = Convert.ToInt32(linha["totalStudents"]);

                    listClassroom.Add(obj);
                }

                return listClassroom;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ClassroomModel LoadDataClassroom(int idClassroom)
        {
            appClassroom = new ClassroomApplication();
            ClassroomModel classroomModel = new ClassroomModel();
            classroomModel.listClassroomStudent = new List<ClassroomStudent>();
            classroomModel.listTeachingPlan = new List<TeachingPlan>();

            try
            {
                foreach (DataRow linha in appClassroom.QuerieClassroom(idClassroom).Rows)
                {
                    if (!linha["idClassroom"].ToString().Equals(""))
                    {
                        classroomModel.idClassroom = Convert.ToInt32(linha["idClassroom"]);
                        classroomModel.nameClassroom = Convert.ToString(linha["nameClassroom"]);
                        classroomModel.shiftClassroom = Convert.ToString(linha["shiftClassroom"]);
                    }
                    else if(!linha["idStudent"].ToString().Equals(""))
                    {
                        ClassroomStudent classroomStudent = new ClassroomStudent();
                        classroomStudent.Student = new Student();

                        classroomStudent.idStudent = Convert.ToInt32(linha["idStudent"]);
                        classroomStudent.Student.registrationNumber = Convert.ToString(linha["registrationNumber"]);
                        classroomStudent.Student.nameStudent = Convert.ToString(linha["nameStudent"]);

                        classroomModel.listClassroomStudent.Add(classroomStudent);
                    }
                    else if (!linha["idTeachingPlan"].ToString().Equals(""))
                    {
                        TeachingPlan teachingPlan = new TeachingPlan();

                        teachingPlan.idTeachingPlan = Convert.ToInt32(linha["idTeachingPlan"]);
                        teachingPlan.activityDescription = Convert.ToString(linha["activityDescription"]);

                        classroomModel.listTeachingPlan.Add(teachingPlan);
                    }
                }

                return classroomModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}