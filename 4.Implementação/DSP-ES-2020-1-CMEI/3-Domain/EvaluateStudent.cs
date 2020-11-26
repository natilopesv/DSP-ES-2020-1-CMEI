using System;
using System.Collections.Generic;

namespace _3_Domain
{
    public class EvaluateStudent
    {
        public int idEvaluateStudent { get; set; }

        public int idClassroom { get; set; }

        public int idStudent { get; set; }

        public DateTime evaluationDate { get; set; }

        public int idLoginAccess { get; set; }
        

        public List<EvaluateStudentGrade> listEvaluateStudentGrade { get; set; }
    }
}
