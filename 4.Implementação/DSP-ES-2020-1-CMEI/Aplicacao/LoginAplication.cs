using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication
{
    public class LoginAplication
    {
        private Context context;
        private SqlConnection connection;
        private SqlCommand cmd;


        /* Queries
         */

        public DataTable QuerieLogin(string emailAccess, string passwordAccess)
        {
            DataTable dt;
            context = new Context();
            context.CleanParameter();

            context.AddParameter("@Action", 4);
            context.AddParameter("@emailAccess", emailAccess);
            context.AddParameter("@passwordAccess", passwordAccess);

            dt = new DataTable();
            dt = context.ExecuteCommandReturnWithParameter(CommandType.StoredProcedure, "UspCrudLoginAccess");
            return dt;
        }

        public DataTable SearchIdLoginAccessWithEmail(string emailAccess)
        {
            DataTable dt;
            context = new Context();
            context.CleanParameter();

            context.AddParameter("@Action", 5);
            context.AddParameter("@emailAccess", emailAccess);

            dt = new DataTable();
            dt = context.ExecuteCommandReturnWithParameter(CommandType.StoredProcedure, "UspCrudLoginAccess");
            return dt;
        }
    }
}
