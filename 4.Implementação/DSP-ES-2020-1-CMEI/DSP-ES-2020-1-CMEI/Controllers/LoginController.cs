using Aplication;
using DSP_ES_2020_1_CMEI.Domain;
using DSP_ES_2020_1_CMEI.Enums;
using DSP_ES_2020_1_CMEI.Models;
using DSP_ES_2020_1_CMEI.Util;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace DSP_ES_2020_1_CMEI.Controllers
{
    public class LoginController : Controller
    {
        /* Global variables
         */

        private LoginModel loginBusinessModel;


        /* Views
 */

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LoginModel formLogin)
        {
            loginBusinessModel = new LoginModel();

            try
            {
                if (loginBusinessModel.IsValidLogin(formLogin))
                {
                    //FormsAuthentication
                    FormsAuthentication.SetAuthCookie(formLogin.emailAccess, false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.MessageType = MessageType.Error;
                    ViewBag.Message = Message.ErrorInvalidLogin;
                }
                
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.MessageType = MessageType.Error;
                ViewBag.Message = ex.Message;

                return View();
            }
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            try
            {
                FormsAuthentication.SignOut();

                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();

                return RedirectToAction("SignIn", "Login");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}