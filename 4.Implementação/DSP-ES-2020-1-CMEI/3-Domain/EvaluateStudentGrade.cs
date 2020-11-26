namespace _3_Domain
{
    public class EvaluateStudentGrade
    {
        public int idEvaluateStudent { get; set; }

        public int idTeachingPlan { get; set; }

        public decimal grade { get; set; }


        public TeachingPlan TeachingPlan { get; set; }
    }
}
