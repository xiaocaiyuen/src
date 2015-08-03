using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunz.Data.EDT;
using Lunz.Data;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using Lunz.Services;
using System.Web.Http.Controllers;
using Lunz.Web.Mvc;

namespace Lunz.Services.EDT
{
    /// <summary>
    /// 处理 Api_Application 数据实体的 Service 接口
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// 验证 AppKey
        /// </summary>
        /// <param name="appKey">AppKey</param>
        /// <param name="actionContext">ActionContext</param>
        /// <returns>成功返回 true, 否则返回 false。</returns>
        Task<bool> Check(Guid appKey, HttpActionContext actionContext);
    }
    /// <summary>
    /// 处理 Api_Application 数据实体的 Service
    /// </summary>
    public class ApplicationService : ServiceBase, IApplicationService
    {
        /// <summary>
        /// 验证 AppKey
        /// </summary>
        /// <param name="appKey">AppKey</param>
        /// <param name="actionContext">ActionContext</param>
        /// <returns>成功返回 true, 否则返回 false。</returns>
        public virtual async Task<bool> Check(Guid appKey, HttpActionContext actionContext)
        {
            var appKeyEntity = DataContext.Api_ApplicationKey.FirstOrDefault(x => x.Id == appKey);
            if (appKeyEntity != null
                && appKeyEntity.Deleted == false
                && appKeyEntity.Api_Application.Deleted == false
                && appKeyEntity.Api_Application.Enabled == true)
            {
                var context = actionContext.Request.GetHttpContext();
                var domain = context.Request.UrlReferrer == null ? null : context.Request.UrlReferrer.Authority;
                var ipAddress = actionContext.Request.GetClientIpAddress();
                if (!string.IsNullOrWhiteSpace(appKeyEntity.Api_Application.Domain)
                    && string.Compare(domain, appKeyEntity.Api_Application.Domain, true) != 0)
                {
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(appKeyEntity.Api_Application.IPAddress)
                    && string.Compare(ipAddress, appKeyEntity.Api_Application.IPAddress, true) != 0)
                {
                    return false;
                }

                //var log = new Api_ApplicationLog
                //{
                //    Api_Application = appKeyEntity.Api_Application,
                //    Api_ApplicationKey = appKeyEntity,
                //    AccessURL = WebContext.Request.RawUrl,
                //    IPAddress = ipAddress,
                //    Browser = WebContext.Request.Browser.Browser,
                //};

                //DataContext.Api_ApplicationLog.Add(log);
                //DataContext.SaveChanges();
                //DataContext.SaveChangesAsync();//.ConfigureAwait(false);
                //await DataContext.SaveChangesAsync(false);

                return true;
            }

            return false;
        }
    }
}
