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
                foreach (DataRow linha in appClassroom.QuerieClassroom(idLoginAccess).Rows)
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

    }
}