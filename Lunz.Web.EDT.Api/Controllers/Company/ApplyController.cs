using Lunz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lunz.Web.EDT.Api.Controllers.Company
{
    public class ApplyController : BaseApiController
    {
        [ActionName("Add")]
        [HttpGet]
        public WebApiResult<string> ApplyAdd()
        {
            WebApiResult<string> result = new WebApiResult<string>();
            result.Data = "销量第一";
            return result;
        }
    }
}
