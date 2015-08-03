using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunz.Web.EDT.Api.AutoMapper
{
    /// <summary>
    /// Profile 集中配置类
    /// </summary>
    public class Configuration : Profile
    {
        /// <summary>
        /// 配置加载方法
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.AddProfile<BasicProfile>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
