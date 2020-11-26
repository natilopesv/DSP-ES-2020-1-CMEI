using _1_Repository;
using _3_Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _2_Application
{
    public class ClassroomApplication
    {
        private Context context;
        private SqlConnection connection;
        private SqlCommand cmd;


        /* Actions
 */

        public string InsertListClassroom(Classroom classroom)
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
                cmd.CommandText = "UspCrudClassroom";
                context.CleanParameter();

                context.AddParameter("@nameClassroom", classroom.nameClassroom);
                context.AddParameter("@shiftClassroom", classroom.shiftClassroom);
                context.AddParameter("@idLoginAccess", classroom.idLoginAccess);

                context.AddParameter("@Action", 1);

                //Adicionar Parametros
                foreach (SqlParameter itemParametros in context.parameterCollection)
                {
                    cmd.Parameters.Add(new SqlParameter(itemParametros.ParameterName, itemParametros.Value));
                }

                retorno = "";
                retorno = cmd.ExecuteScalar().ToString();
                int idClassroom = Convert.ToInt32(retorno);


                foreach (var item in classroom.listClassroomStudent)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UspCrudClassroomStudent";
                    context.CleanParameter();

                    context.AddParameter("@idClassroom", idClassroom);
                    context.AddParameter("@idStudent", item.idStudent);

                    context.AddParameter("@Action", 1);

                    //Adicionar Parametros
                    foreach (SqlParameter itemParametros in context.parameterCollection)
                    {
                        cmd.Parameters.Add(new SqlParameter(itemParametros.ParameterName, itemParametros.Value));
                    }

                    retorno = "";
                    retorno = cmd.ExecuteScalar().ToString();
                    validation = Convert.ToInt32(retorno);
                }

                foreach (var item in classroom.listTeachingPlan)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UspCrudTeachingPlan";
                    context.CleanParameter();

                    context.AddParameter("@idClassroom", idClassroom);
                    context.AddParameter("@activityDescription", item.activityDescription);

                    context.AddParameter("@Action", 1);

                    //Adicionar Parametros
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

        public DataTable QuerieClassroom(int idLoginAccess)
        {
            DataTable dt;
            context = new Context();
            context.CleanParameter();

            context.AddParameter("@Action", 4);
            context.AddParameter("@idLoginAccess", idLoginAccess);

            dt = new DataTable();
            dt = context.ExecuteCommandReturnWithParameter(CommandType.StoredProcedure, "UspCrudClassroom");
            return dt;
        }
    }
}
