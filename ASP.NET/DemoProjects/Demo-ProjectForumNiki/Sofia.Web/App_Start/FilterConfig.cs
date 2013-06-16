using Sofia.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace Sofia.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ValidateInputAttribute(false));
            filters.Add(new LogAttribute());
        }
    }
}