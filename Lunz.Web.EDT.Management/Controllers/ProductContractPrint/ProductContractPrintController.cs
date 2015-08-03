using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lunz.Web.EDT.Management.Controllers
{
    public class ProductContractPrintController : BaseController
    {
        // GET: Products
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult Print()
        {
            return PartialView();
        }
    }
}