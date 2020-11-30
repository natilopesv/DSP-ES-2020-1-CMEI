using _1_Repository;
using _3_Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _2_Application
{
    public class StudentApplication
    {
        private Context context;
        private SqlConnection connection;
        private SqlCommand cmd;


        /* Actions
 */

        public string InsertListStudent(List<Student> list, int idLoginAccess)
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
                foreach (var item in list)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UspCrudStudent";
                    context.CleanParameter();

                    context.AddParameter("@registrationNumber", item.registrationNumber);
                    context.AddParameter("@nameStudent", item.nameStudent);
                    context.AddParameter("@birthDate", item.birthDate);
                    context.AddParameter("@idLoginAccess", idLoginAccess);

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

        public DataTable QuerieAllStudent(int idLoginAccess)
        {
            DataTable dt;
            context = new Context();
            context.CleanParameter();

            context.AddParameter("@Action", 4);
            context.AddParameter("@idLoginAccess", idLoginAccess);

            dt = new DataTable();
            dt = context.ExecuteCommandReturnWithParameter(CommandType.StoredProcedure, "UspCrudStudent");
            return dt;
        }

        public DataTable QuerieStudent(int idStudent)
        {
            DataTable dt;
            context = new Context();
            context.CleanParameter();

            context.AddParameter("@Action", 5);
            context.AddParameter("@idStudent", idStudent);

            dt = new DataTable();
            dt = context.ExecuteCommandReturnWithParameter(CommandType.StoredProcedure, "UspCrudStudent");
            return dt;
        }

    }
}
