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
    public class ProductDefinitionController : BaseApiController
    {
        private readonly IProductDefinitionService _ProductDefinitionService;
        /// <summary>
        /// 初始化 SeatController 类的新实例
        /// </summary>
        /// <param name="productDefinition"></param>
        public ProductDefinitionController(IProductDefinitionService productDefinition)
        {
            _ProductDefinitionService = productDefinition;
        }
        /// <summary>
        /// 获取 产品信息 列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>成功返回 产品信息 列表</returns>
        [ActionName("List")]
        public WebApiPagingResult<IQueryable<Product_ProductDefinition>> GetList([FromUri]PagingOptions paging)
        {
            return _ProductDefinitionService.List(paging);
        }

        [ActionName("GetProductDefinitionModel")]
        [HttpPost]
        public WebApiResult<Product_ProductDefinition> GetProductInfo()
        {
            WebApiResult<Product_ProductDefinition> result = new WebApiResult<Product_ProductDefinition>();
            Product_ProductDefinition productInfo = new Product_ProductDefinition();
            result.Data = productInfo;
            result.Success = true;
            return result;
        }
        /// <summary>
        /// 新建 产品信息
        /// </summary>
        /// <param name="entity">要新建的产品信息参数</param>
        /// <returns>成功返回新建的产品信息参数</returns>
        [ActionName("Add")]
        public WebApiResult<Product_ProductDefinition> PostAdd([FromBody]Product_ProductDefinition entity)
        {
            ILogService logService = new LogService();
            logService.Info("新增一条产品数据");
            return _ProductDefinitionService.Add(entity);
        }

        /// <summary>
        /// 更新 汽车座位参数
        /// </summary>
        /// <param name="entity">要更新的汽车座位参数</param>
        /// <returns>成功返回更新后的汽车座位参数</returns>
        [ActionName("Update")]
        public WebApiResult<Product_ProductDefinition> PostUpdate([FromBody]Product_ProductDefinition entity)
        {
            ILogService logService = new LogService();
            logService.Info("更新一条产品数据");
            return _ProductDefinitionService.Update(entity);
        }
        /// <summary>
        /// 删除 汽车座位参数
        /// </summary>
        /// <param name="id">要删除的汽车座位参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("Delete")]
        public WebApiResult PostDelete([FromBody]Guid[] id)
        {
            ILogService logService = new LogService();
            logService.Info("删除一条车辆数据");
            return _ProductDefinitionService.Delete(id);
        }
    }
}