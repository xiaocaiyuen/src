using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lunz.Web.EDT.Management.Controllers
{
    [CustomAuthorize(true)]
    public abstract class BaseController : Controller
    {
    }
}
