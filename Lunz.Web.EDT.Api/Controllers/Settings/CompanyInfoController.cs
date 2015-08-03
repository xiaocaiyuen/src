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
    /// 数据字典
    /// </summary>
    public class CompanyInfoController : BaseApiController
    {
        private readonly ICompanyInfoService _CompanyInfoService;

        /// <summary>
        /// 初始化 CompanyInfoController 类的新实例
        /// </summary>
        /// <param name="CompanyInfoService"></param>
        public CompanyInfoController(ICompanyInfoService CompanyInfoService)
        {
            _CompanyInfoService = CompanyInfoService;
        }
        /// <summary>
        /// 获取 数据字典 列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>成功返回 '数据字典' 列表</returns>
        [ActionName("List")]
        public WebApiPagingResult<IQueryable<Basic_CompanyInfo>> GetList([FromUri]PagingOptions paging)
        {
            return _CompanyInfoService.List(paging);
        }

        /// <summary>
        /// 新建 数据字典参数
        /// </summary>
        /// <param name="entity">要新建的数据字典参数</param>
        /// <returns>成功返回新建的数据字典参数</returns>
        [ActionName("Add")]
        public WebApiResult<Basic_CompanyInfo> PostAdd([FromBody]Basic_CompanyInfo entity)
        {
            return _CompanyInfoService.Add(entity);
        }
        /// <summary>
        /// 更新 数据字典参数
        /// </summary>
        /// <param name="entity">要更新的数据字典参数</param>
        /// <returns>成功返回更新后的数据字典参数</returns>
        [ActionName("Update")]
        public WebApiResult<Basic_CompanyInfo> PostUpdate([FromBody]Basic_CompanyInfo entity)
        {
            return _CompanyInfoService.Update(entity);
        }
        /// <summary>
        /// 删除 数据字典参数
        /// </summary>
        /// <param name="id">要删除的数据字典参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("Delete")]
        public WebApiResult PostDelete([FromBody]Guid[] id)
        {
            return _CompanyInfoService.Delete(id);
        }
        /// <summary>
        /// 撤消删除 数据字典参数
        /// </summary>
        /// <param name="id">要撤消删除的数据字典参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("Restore")]
        public WebApiResult PostRestore([FromBody]Guid[] id)
        {
            return _CompanyInfoService.Restore(id);
        }
        /// <summary>
        /// 将数据字典参数向上移动
        /// </summary>
        /// <param name="id">要移动的数据字典参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("MoveUp")]
        public WebApiResult PostMoveUp([FromBody]Guid[] id)
        {
            return _CompanyInfoService.MoveUp(id);
        }
        /// <summary>
        /// 将数据字典参数向下移动
        /// </summary>
        /// <param name="id">要移动的数据字典参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("MoveDown")]
        public WebApiResult PostMoveDown([FromBody]Guid[] id)
        {
            return _CompanyInfoService.MoveDown(id);
        }
    }
}
