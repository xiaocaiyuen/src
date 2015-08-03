using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Lunz.Web.EDT.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
