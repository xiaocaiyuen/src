using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lunz.Web.EDT.Management.Controllers.Business
{
    public class ApplyController : Controller
    {
        // GET: Apply
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return PartialView();
        }

        public ActionResult Calculator()
        {
            return PartialView();
        }

        public ActionResult PersonageNew()
        {
            return PartialView();
        }

        public ActionResult CompanyNew()
        {
            return PartialView();
        }

        public ActionResult CompanyEdit()
        {
            return PartialView("CompanyNew");
        }

        public ActionResult PersonageEdit()
        {
            return PartialView("PersonageNew");
        }

        public ActionResult Edit()
        {
            return PartialView("New");
        }

        public ActionResult Detail()
        {
            return PartialView();
        }
    }
}