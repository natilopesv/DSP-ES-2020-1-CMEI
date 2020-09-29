using System.Web;
using System.Web.Mvc;

namespace DSP_ES_2020_1_CMEI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
