using System;
using System.Data;
using System.Data.SqlClient;

namespace _1_Repository
{
    public class Context
    {
        private SqlConnection connection = null;
        private SqlCommand cmd = null;

        public SqlParameterCollection parameterCollection = new SqlCommand().Parameters;
        string msgId = "";


        public string StringConnection()
        {
            //O nome do banco é a própria url
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=localhost\EMPRESA;Initial Catalog=DSP-ES-2020-1-CMEI; User ID=sa;Password=alvotec@9219;";

            return connection.ConnectionString;
        }

        public void CleanParameter()
        {
            parameterCollection.Clear();
        }

        public void AddParameter(string parameterName, object parameterValue)
        {
            parameterCollection.Add(new SqlParameter(parameterName, parameterValue));
        }

        public string ExecuteCommand(CommandType commandType, string storeProcedure)
        {
            try
            {
                connection = new SqlConnection(StringConnection());
                connection.Open();

                cmd = connection.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = storeProcedure;
                cmd.CommandTimeout = 7200; // em segundos

                //Adicionar Parametros
                foreach (SqlParameter item in parameterCollection)
                {
                    cmd.Parameters.Add(new SqlParameter(item.ParameterName, item.Value));
                }

                msgId = "";
                msgId = cmd.ExecuteScalar().ToString();
                return msgId;
            }
            catch (Exception)
            {
                throw;
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

        public DataTable ExecuteCommandReturn(CommandType commandType, string storeProcedure)
        {
            try
            {
                connection = new SqlConnection(StringConnection());
                connection.Open();

                cmd = connection.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = storeProcedure;
                cmd.CommandTimeout = 7200; // em segundos

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception)
            {
                throw;
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

        public DataTable ExecuteCommandReturnWithParameter(CommandType commandType, string storeProcedure)
        {
            try
            {
                connection = new SqlConnection(StringConnection());
                connection.Open();

                cmd = connection.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = storeProcedure;
                cmd.CommandTimeout = 7200; // em segundos

                foreach (SqlParameter item in parameterCollection)
                {
                    cmd.Parameters.Add(new SqlParameter(item.ParameterName, item.Value));
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception)
            {
                throw;
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
    }
}
