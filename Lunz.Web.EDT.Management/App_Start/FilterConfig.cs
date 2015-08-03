using System.Web;
using System.Web.Mvc;

namespace Lunz.Web.EDT.Management
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
