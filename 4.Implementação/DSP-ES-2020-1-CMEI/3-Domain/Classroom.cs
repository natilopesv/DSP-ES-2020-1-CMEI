using System.Collections.Generic;

namespace _3_Domain
{
    public class Classroom
    {
        public int idClassroom { get; set; }

        public string nameClassroom { get; set; }

        public string shiftClassroom { get; set; }

        public int idLoginAccess { get; set; }


        public ClassroomStudent ClassroomStudent { get; set; }

        public List<ClassroomStudent> listClassroomStudent { get; set; }
    }
}
