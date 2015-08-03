using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lunz.Web.EDT.Management.Controllers
{
    public class NavigationController : BaseController
    {
        public ActionResult Header()
        {
            return PartialView();
        }

        public ActionResult Sidebar()
        {
            return PartialView();
        }

        public ActionResult ThemePanel()
        {
            return PartialView();
        }

        public ActionResult QuickSidebar()
        {
            return PartialView();
        }

        public ActionResult Footer()
        {
            return PartialView();
        }
    }
}
