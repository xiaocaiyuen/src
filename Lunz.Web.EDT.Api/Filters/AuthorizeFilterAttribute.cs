using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lunz.Web.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Lunz.Services.EDT;

namespace Lunz.Web.EDT.Api.Filters
{
    public class AuthorizeFilterAttribute : AuthorizeAttribute
    {
        [Dependency]
        public IApplicationService ApplicationService { get; set; }
        [Dependency]
        public IMembershipService MembershipService { get; set; }

        public AuthorizeFilterAttribute()
        {

        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var appKeyString = actionContext.Request.GetQueryString("appKey");
            Guid appKey;
            Guid.TryParse(appKeyString, out appKey);
            if (string.IsNullOrWhiteSpace(appKeyString)
                || appKey == Guid.Empty
                || !ApplicationService.Check(appKey, actionContext).Result)
            {
                throw new UnauthorizedAccessException(
                    string.Format("参数 appKey '{0}' 无效.", appKey));
            }
            else
            {
                bool allowAnonymous = false;
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Count > 0)
                {
                    allowAnonymous = true;
                }

                var authToken = actionContext.Request.GetQueryString("authToken");
                if (!string.IsNullOrWhiteSpace(authToken))
                {
                    var authResult = MembershipService.ValidateAuthToken(appKey, Guid.Parse(authToken));
                    if (authResult.Success)
                    {
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                            new WebApiIdentity(authResult.Data.UserId.ToString()), new string[] { });
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("validate token: " + authResult.AllMessages);
                    }
                }

                //if (!allowAnonymous
                //    && !MembershipService.CheckApiPermission(
                //            actionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                //            actionContext.ActionDescriptor.ActionName))
                //{
                //    throw new UnauthorizedAccessException("您没有权限访问此接口。");
                //}

                if (!allowAnonymous && HttpContext.Current.User.Identity.IsAuthenticated == false)
                {
                    throw new UnauthorizedAccessException("未登录或凭证已过期。");
                }
            }
        }
    }
}
