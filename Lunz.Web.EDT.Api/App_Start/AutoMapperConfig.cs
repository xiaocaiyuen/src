using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunz.Web.EDT.Api
{
    public class AutoMapperConfig
    {
        public static void RegisterAutoMapper()
        {
            AutoMapper.Configuration.Configure();
        }
    }
}
