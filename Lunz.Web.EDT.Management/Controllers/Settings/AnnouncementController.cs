﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lunz.Web.EDT.Management.Controllers
{
    public class AnnouncementController:BaseController
    {
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult New()
        {
            return PartialView();
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