using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lunz.Web.EDT.Management
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public virtual bool Warning { get; set; }

        public CustomAuthorizeAttribute()
        {

        }

        public CustomAuthorizeAttribute(bool warning)
        {
            Warning = warning;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var attrs = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (attrs.Any(x => x is AllowAnonymousAttribute))
            {
                return;
            }
            attrs = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(true);
            if (attrs.Any(x => x is AllowAnonymousAttribute))
            {
                return;
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (Warning)
                {
                    filterContext.Result = new PartialViewResult()
                    {
                        ViewName = "Unauthorized",
                    };
                }
                else
                {
                    base.OnAuthorization(filterContext);
                }
            }
        }
    }
}
