using Lunz.Web.EDT.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Lunz.Web.EDT.Api.Controllers
{
    [AuthorizeFilter]
    public abstract class BaseApiController: ApiController
    {
    }
}
