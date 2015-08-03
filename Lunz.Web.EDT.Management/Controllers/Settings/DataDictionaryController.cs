using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lunz.Web.EDT.Management.Controllers
{
    public class DataDictionaryController : BaseController
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult New()
        {
            return PartialView();
        }

        public ActionResult Sub()
        {
            return PartialView();
        }

        public ActionResult Edit()
        {
            return PartialView("New");
        }
    }
}