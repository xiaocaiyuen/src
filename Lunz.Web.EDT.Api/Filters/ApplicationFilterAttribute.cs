using Lunz.Services.EDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lunz.Web.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;

namespace Lunz.Web.EDT.Api.Filters
{
    public class ApplicationFilterAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IApplicationService ApplicationService { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
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
                base.OnActionExecuting(actionContext);
            }

        }

        public override System.Threading.Tasks.Task OnActionExecutingAsync(System.Web.Http.Controllers.HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}