using System.Web.Mvc;

namespace DSP_ES_2020_1_CMEI.AuthorizeCustom
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                        {
                                { "controller", "Login" },
                                { "action", "SignIn" },
                        });
            }
        }
    }
}