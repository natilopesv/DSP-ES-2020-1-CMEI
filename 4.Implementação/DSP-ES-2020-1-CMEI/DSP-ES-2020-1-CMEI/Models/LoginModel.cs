using _2_Application;
using _3_Domain;
using System;
using System.Data;

namespace DSP_ES_2020_1_CMEI.Models
{
    public class LoginModel: LoginAccess
    {
        /* View attr
         */

        /* Business rules

 */

        private LoginApplication appLogin;

        public bool IsValidLogin(LoginModel loginModel)
        {
            try
            {
                appLogin = new LoginApplication();

                if (appLogin.QuerieLogin(loginModel.emailAccess, loginModel.passwordAccess).Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetIdLoginAccessWithEmail(string emailAccess)
        {
            try
            {
                appLogin = new LoginApplication();
                int idLoginAccess = 0;

                foreach (DataRow linha in appLogin.SearchIdLoginAccessWithEmail(emailAccess).Rows)
                {
                    idLoginAccess = Convert.ToInt32(linha["idLoginAccess"]);
                }

                return idLoginAccess;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}