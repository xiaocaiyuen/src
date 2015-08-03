using Lunz.Data.EDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunz.Services.EDT
{
    /// <summary>
    /// 处理 Logs 数据实体的 Service 接口
    /// </summary>
    public interface ILogsService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        WebApiPagingResult<IQueryable<Basic_Logs>> List(PagingOptions paging);
    }
    public class LogsService : ServiceBase, ILogsService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        public WebApiPagingResult<IQueryable<Basic_Logs>> List(PagingOptions paging)
        {
            var result = new WebApiPagingResult<IQueryable<Basic_Logs>>
            {
                Data = from item in DataContext.Basic_Logs
                       orderby item.LogTime descending
                       select item
            };

            return result.AsPaging(paging);
        }
    }
}
