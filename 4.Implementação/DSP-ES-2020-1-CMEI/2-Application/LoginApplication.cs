using _1_Repository;
using System.Data;

namespace _2_Application
{
    public class LoginApplication
    {
        private Context context;


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
