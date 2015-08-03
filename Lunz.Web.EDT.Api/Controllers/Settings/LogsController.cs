using Lunz.Data.EDT;
using Lunz.Services;
using Lunz.Services.EDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Lunz.Web.EDT.Api.Controllers
{
    public class LogsController:BaseApiController
    {
        private readonly ILogsService _logsService;

        /// <summary>
        /// 初始化 LogsController 类的新实例
        /// </summary>
        /// <param name="seatServcie"></param>
        public LogsController(ILogsService logsService)
        {
            _logsService = logsService;
        }

        /// <summary>
        /// 获取 操作日志 列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>成功返回 '操作日志' 列表</returns>
        [ActionName("List")]
        public WebApiPagingResult<IQueryable<Basic_Logs>> GetList([FromUri]PagingOptions paging)
        {
            return _logsService.List(paging);
        }
    }
}