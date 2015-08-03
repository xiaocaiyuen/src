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
    public class BankInfoController:BaseApiController
    {
        private readonly IBankInfoService _bankInfoService;

        /// <summary>
        /// 初始化 BankInfoController 类的新实例
        /// </summary>
        /// <param name="bankInfoService"></param>
        public BankInfoController(IBankInfoService bankInfoService)
        {
            _bankInfoService = bankInfoService;
        }

        /// <summary>
        /// 获取 银行信息 列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>成功返回 '银行信息' 列表</returns>
        [ActionName("List")]
        public WebApiPagingResult<IQueryable<Basic_BankInfo>> GetList([FromUri]PagingOptions paging)
        {
            return _bankInfoService.List(paging);
        }

        /// <summary>
        /// 新建 银行信息
        /// </summary>
        /// <param name="entity">要新建的银行信息参数</param>
        /// <returns>成功返回新建的银行信息参数</returns>
        [ActionName("Add")]
        public WebApiResult<Basic_BankInfo> PostAdd([FromBody]Basic_BankInfo entity)
        {
            ILogService logService = new LogService();
            logService.Info("新增一条银行信息");
            return _bankInfoService.Add(entity);
        }

        /// <summary>
        /// 更新 银行信息
        /// </summary>
        /// <param name="entity">要更新的银行信息参数</param>
        /// <returns>成功返回更新后的银行信息参数</returns>
        [ActionName("Update")]
        public WebApiResult<Basic_BankInfo> PostUpdate([FromBody]Basic_BankInfo entity)
        {
            ILogService logService = new LogService();
            logService.Info("更新一条银行信息");
            return _bankInfoService.Update(entity);
        }
        /// <summary>
        /// 删除 银行信息
        /// </summary>
        /// <param name="id">要删除的银行信息主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("Delete")]
        public WebApiResult PostDelete([FromBody]Guid[] id)
        {
            ILogService logService = new LogService();
            logService.Info("删除一条银行信息");
            return _bankInfoService.Delete(id);
        }
    }
}