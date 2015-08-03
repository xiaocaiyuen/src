using Lunz.Data.EDT;
using Lunz.Services;
using Lunz.Services.EDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lunz.Web.EDT.Api.Controllers
{
    /// <summary>
    /// 业务申请
    /// </summary>
    public class ApplyController : BaseApiController
    {
        /// <summary>
        /// 获取 业务申请 列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>成功返回 '业务申请' 列表</returns>
        [ActionName("List")]
        public WebApiPagingResult<IQueryable<Basic_CompanyInfo>> GetList([FromUri]PagingOptions paging)
        {
            return null;
        }

        /// <summary>
        /// 新建 业务申请参数
        /// </summary>
        /// <param name="entity">要新建的业务申请参数</param>
        /// <returns>成功返回新建的业务申请参数</returns>
        [ActionName("Add")]
        public WebApiResult<Basic_CompanyInfo> PostAdd([FromBody]Basic_CompanyInfo entity)
        {
            return null;
        }
        /// <summary>
        /// 更新 业务申请参数
        /// </summary>
        /// <param name="entity">要更新的业务申请参数</param>
        /// <returns>成功返回更新后的业务申请参数</returns>
        [ActionName("Update")]
        public WebApiResult<Basic_CompanyInfo> PostUpdate([FromBody]Basic_CompanyInfo entity)
        {
            return null;
        }
        /// <summary>
        /// 删除 业务申请参数
        /// </summary>
        /// <param name="id">要删除的业务申请参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("Delete")]
        public WebApiResult PostDelete([FromBody]Guid[] id)
        {
            return null;
        }
        /// <summary>
        /// 撤消删除 业务申请参数
        /// </summary>
        /// <param name="id">要撤消删除的业务申请参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("Restore")]
        public WebApiResult PostRestore([FromBody]Guid[] id)
        {
            return null;
        }
        /// <summary>
        /// 将业务申请参数向上移动
        /// </summary>
        /// <param name="id">要移动的业务申请参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("MoveUp")]
        public WebApiResult PostMoveUp([FromBody]Guid[] id)
        {
            return null;
        }
        /// <summary>
        /// 将业务申请参数向下移动
        /// </summary>
        /// <param name="id">要移动的业务申请参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("MoveDown")]
        public WebApiResult PostMoveDown([FromBody]Guid[] id)
        {
            return null;
        }
    }
}
