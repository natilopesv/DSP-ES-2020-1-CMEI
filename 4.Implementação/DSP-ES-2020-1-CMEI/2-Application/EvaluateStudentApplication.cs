using _1_Repository;
using _3_Domain;
using System;
using System.Data;
using System.Data.SqlClient;

namespace _2_Application
{
    public class EvaluateStudentApplication
    {
        private Context context;
        private SqlConnection connection;
        private SqlCommand cmd;


        /* Actions
 */

        public string InsertEvaluateStudent(EvaluateStudent evaluateStudent)
        {
            string retorno = "";
            int validation = 0;

            context = new Context();
            connection = new SqlConnection(context.StringConnection());
            connection.Open();

            SqlTransaction insertValidation = connection.BeginTransaction();
            cmd = connection.CreateCommand();

            cmd.Transaction = insertValidation;
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                cmd.Parameters.Clear();
                cmd.CommandText = "UspCrudEvaluateStudent";
                context.CleanParameter();

                context.AddParameter("@idClassroom", evaluateStudent.idClassroom);
                context.AddParameter("@idStudent", evaluateStudent.idStudent);
                context.AddParameter("@evaluationDate", evaluateStudent.evaluationDate);
                context.AddParameter("@idLoginAccess", evaluateStudent.idLoginAccess);

                context.AddParameter("@Action", 1);

                
                foreach (SqlParameter itemParametros in context.parameterCollection)
                {
                    cmd.Parameters.Add(new SqlParameter(itemParametros.ParameterName, itemParametros.Value));
                }

                retorno = "";
                retorno = cmd.ExecuteScalar().ToString();
                int idEvaluateStudent = Convert.ToInt32(retorno);


                foreach (var item in evaluateStudent.listEvaluateStudentGrade)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UspCrudEvaluateStudentGrade";
                    context.CleanParameter();

                    context.AddParameter("@idEvaluateStudent", idEvaluateStudent);
                    context.AddParameter("@idTeachingPlan", item.idTeachingPlan);
                    context.AddParameter("@grade", item.grade);

                    context.AddParameter("@Action", 1);

                    
                    foreach (SqlParameter itemParametros in context.parameterCollection)
                    {
                        cmd.Parameters.Add(new SqlParameter(itemParametros.ParameterName, itemParametros.Value));
                    }

                    retorno = "";
                    retorno = cmd.ExecuteScalar().ToString();
                    validation = Convert.ToInt32(retorno);
                }

                insertValidation.Commit();

                return retorno;
            }
            catch (Exception ex)
            {
                if (retorno == "")
                {
                    retorno = ex.Message;
                }

                insertValidation.Rollback();

                return retorno;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        public string UpdateEvaluateStudent(EvaluateStudent evaluateStudent)
        {
            string retorno = "";
            int validation = 0;

            context = new Context();
            connection = new SqlConnection(context.StringConnection());
            connection.Open();

            SqlTransaction insertValidation = connection.BeginTransaction();
            cmd = connection.CreateCommand();

            cmd.Transaction = insertValidation;
            cmd.CommandType = CommandType.StoredProcedure;
                      
            try
            {

                cmd.Parameters.Clear();
                cmd.CommandText = "UspCrudEvaluateStudent";
                context.CleanParameter();

                context.AddParameter("@idEvaluateStudent", evaluateStudent.idEvaluateStudent);
                context.AddParameter("@idClassroom", evaluateStudent.idClassroom);
                context.AddParameter("@idStudent", evaluateStudent.idStudent);
                context.AddParameter("@evaluationDate", evaluateStudent.evaluationDate);
                context.AddParameter("@idLoginAccess", evaluateStudent.idLoginAccess);

                context.AddParameter("@Action", 2);

                
                foreach (SqlParameter itemParametros in context.parameterCollection)
                {
                    cmd.Parameters.Add(new SqlParameter(itemParametros.ParameterName, itemParametros.Value));
                }

                retorno = "";
                retorno = cmd.ExecuteScalar().ToString();
                int idEvaluateStudent = Convert.ToInt32(retorno);

                //Delete evaluate
                cmd.Parameters.Clear();
                cmd.CommandText = "UspCrudEvaluateStudentGrade";
                context.CleanParameter();

                context.AddParameter("@idEvaluateStudent", evaluateStudent.idEvaluateStudent);

                context.AddParameter("@Action", 2);

                foreach (SqlParameter itemParametros in context.parameterCollection)
                {
                    cmd.Parameters.Add(new SqlParameter(itemParametros.ParameterName, itemParametros.Value));
                }

                retorno = "";
                retorno = cmd.ExecuteScalar().ToString();
                validation = Convert.ToInt32(retorno);

                foreach (var item in evaluateStudent.listEvaluateStudentGrade)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UspCrudEvaluateStudentGrade";
                    context.CleanParameter();

                    context.AddParameter("@idEvaluateStudent", idEvaluateStudent);
                    context.AddParameter("@idTeachingPlan", item.idTeachingPlan);
                    context.AddParameter("@grade", item.grade);

                    context.AddParameter("@Action", 1);

                    
                    foreach (SqlParameter itemParametros in context.parameterCollection)
                    {
                        cmd.Parameters.Add(new SqlParameter(itemParametros.ParameterName, itemParametros.Value));
                    }

                    retorno = "";
                    retorno = cmd.ExecuteScalar().ToString();
                    validation = Convert.ToInt32(retorno);
                }

                insertValidation.Commit();

                return retorno;
            }
            catch (Exception ex)
            {
                if (retorno == "")
                {
                    retorno = ex.Message;
                }

                insertValidation.Rollback();

                return retorno;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }


        /* Queries
         */

        public DataTable QuerieEvaluateStudent(int idClassroom)
        {
            DataTable dt;
            context = new Context();
            context.CleanParameter();

            context.AddParameter("@Action", 4);
            context.AddParameter("@idClassroom", idClassroom);

            dt = new DataTable();
            dt = context.ExecuteCommandReturnWithParameter(CommandType.StoredProcedure, "UspCrudEvaluateStudent");
            return dt;
        }

        public DataTable QuerieEvaluateStudentGrade(int idClassroom, int idStudent)
        {
            DataTable dt;
            context = new Context();
            context.CleanParameter();

            context.AddParameter("@Action", 5);
            context.AddParameter("@idClassroom", idClassroom);
            context.AddParameter("@idStudent", idStudent);

            dt = new DataTable();
            dt = context.ExecuteCommandReturnWithParameter(CommandType.StoredProcedure, "UspCrudEvaluateStudent");
            return dt;
        }

        public DataTable QuerieDataEvaluateStudentReport(int idClassroom)
        {
            DataTable dt;
            context = new Context();
            context.CleanParameter();

            context.AddParameter("@Action", 6);
            context.AddParameter("@idClassroom", idClassroom);

            dt = new DataTable();
            dt = context.ExecuteCommandReturnWithParameter(CommandType.StoredProcedure, "UspCrudEvaluateStudent");
            return dt;
        }

    }
}
