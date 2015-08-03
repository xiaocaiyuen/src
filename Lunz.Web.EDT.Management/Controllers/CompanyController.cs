using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lunz.Web.EDT.Management.Controllers
{
    public class CompanyController : BaseController
    {
        // GET: Apply
        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: Apply
        public ActionResult Apply()
        {
            return PartialView();
        }
    }
}