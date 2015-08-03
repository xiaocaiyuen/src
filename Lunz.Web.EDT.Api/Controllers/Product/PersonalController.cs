using Lunz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Lunz.Web.EDT.Api.Controllers
{
    public class PersonalController : BaseApiController
    {
        [ActionName("Apply")]
        [HttpGet]
        public WebApiResult<string> ApplyItems()
        {
            WebApiResult<string> result = new WebApiResult<string>();
            result.Data = "API层数据";
            return result;
        }
    }
}