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
    public class ProductContractController :BaseApiController
    {
      private readonly IProductContractService  _ProductContractService;
        /// <summary>
        /// 初始化 SeatController 类的新实例
        /// </summary>
      /// <param name="productContract"></param>
      public ProductContractController(IProductContractService productContract)
        {
            _ProductContractService = productContract;
        }

      /// <summary>
      /// 获取 产品信息 列表
      /// </summary>
      /// <param name="paging">分页参数</param>
      /// <returns>成功返回 产品信息 列表</returns>
      [ActionName("List")]
      public WebApiPagingResult<IQueryable<Product_ProductContract>> GetList([FromUri]PagingOptions paging)
      {
          return _ProductContractService.List(paging);
      }

      /// <summary>
      /// 获取 产品信息 列表
      /// </summary>
      /// <param name="paging">分页参数</param>
      /// <returns>成功返回 产品信息 列表</returns>
      [ActionName("GetProductContractById")]
      public WebApiResult<Product_ProductContract> GetProductContractById(Guid id)
      {
          return _ProductContractService.SelectInfoById(id);
      }
      /// <summary>
      /// 新建 合同模板
      /// </summary>
      /// <param name="entity">要新建的合同信息参数</param>
      /// <returns>成功返回新建的合同信息参数</returns>
      [ActionName("Add")]
      public WebApiResult<Product_ProductContract> PostAdd([FromBody]Product_ProductContract entity)
      {
          ILogService logService = new LogService();
          logService.Info("新增一个合同信息");
          return _ProductContractService.Add(entity);
      }

      /// <summary>
      /// 更新 合同模板参数
      /// </summary>
      /// <param name="entity">要更新的合同模板参数</param>
      /// <returns>成功返回更新后的合同模板参数</returns>
      [ActionName("Update")]
      public WebApiResult<Product_ProductContract> PostUpdate([FromBody]Product_ProductContract entity)
      {
          ILogService logService = new LogService();
          logService.Info("更新一个合同信息");
          return _ProductContractService.Update(entity);
      }
      /// <summary>
      /// 删除 合同模板参数
      /// </summary>
      /// <param name="id">要删除的合同模板参数主键列表</param>
      /// <returns>成功返回 true；否则返回 false。</returns>
      [ActionName("Delete")]
      public WebApiResult PostDelete([FromBody]Guid[] id)
      {
          ILogService logService = new LogService();
          logService.Info("删除一条合同数据");
          return _ProductContractService.Delete(id);
      }
    }
}