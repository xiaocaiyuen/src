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
    public class DataDictionaryController : BaseApiController
    {
        private readonly IDataDictionaryService _DataDictionaryService;

        /// <summary>
        /// 初始化 DataDictionaryController 类的新实例
        /// </summary>
        /// <param name="dataDictionaryService"></param>
        public DataDictionaryController(IDataDictionaryService dataDictionaryService)
        {
            _DataDictionaryService = dataDictionaryService;
        }
        /// <summary>
        /// 获取 数据字典 列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>成功返回 '数据字典' 列表</returns>
        [ActionName("List")]
        public WebApiPagingResult<IQueryable<Basic_DataDictionary>> GetList([FromUri]PagingOptions paging)
        {
            return _DataDictionaryService.List(paging);
        }

        /// <summary>
        /// 获取 数据字典 列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>成功返回 '数据字典' 列表</returns>
        [ActionName("SubList")]
        public WebApiPagingResult<IQueryable<Basic_DataDictionary>> GetList([FromUri]PagingOptions paging, Guid? ParentId)
        {
            return _DataDictionaryService.SubList(paging, ParentId);
        }
        /// <summary>
        /// 新建 数据字典参数
        /// </summary>
        /// <param name="entity">要新建的数据字典参数</param>
        /// <returns>成功返回新建的数据字典参数</returns>
        [ActionName("Add")]
        public WebApiResult<Basic_DataDictionary> PostAdd([FromBody]Basic_DataDictionary entity)
        {
            return _DataDictionaryService.Add(entity);
        }
        /// <summary>
        /// 更新 数据字典参数
        /// </summary>
        /// <param name="entity">要更新的数据字典参数</param>
        /// <returns>成功返回更新后的数据字典参数</returns>
        [ActionName("Update")]
        public WebApiResult<Basic_DataDictionary> PostUpdate([FromBody]Basic_DataDictionary entity)
        {
            return _DataDictionaryService.Update(entity);
        }
        /// <summary>
        /// 删除 数据字典参数
        /// </summary>
        /// <param name="id">要删除的数据字典参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("Delete")]
        public WebApiResult PostDelete([FromBody]Guid[] id)
        {
            return _DataDictionaryService.Delete(id);
        }
        /// <summary>
        /// 撤消删除 数据字典参数
        /// </summary>
        /// <param name="id">要撤消删除的数据字典参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("Restore")]
        public WebApiResult PostRestore([FromBody]Guid[] id)
        {
            return _DataDictionaryService.Restore(id);
        }
        /// <summary>
        /// 将数据字典参数向上移动
        /// </summary>
        /// <param name="id">要移动的数据字典参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("MoveUp")]
        public WebApiResult PostMoveUp([FromBody]Guid[] id)
        {
            return _DataDictionaryService.MoveUp(id);
        }
        /// <summary>
        /// 将数据字典参数向下移动
        /// </summary>
        /// <param name="id">要移动的数据字典参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("MoveDown")]
        public WebApiResult PostMoveDown([FromBody]Guid[] id)
        {
            return _DataDictionaryService.MoveDown(id);
        }

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        /// <param name="Id">父节点主键Id</param>
        /// <returns>返回数据列表</returns>
        [ActionName("ChildList")]
        public WebApiResult<IQueryable<Basic_DataDictionary>> GetDataDictionaryById(Guid Id)
        {
            return _DataDictionaryService.GetDataDictionaryById(Id);
        }

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        /// <param name="Id">编码Code</param>
        /// <returns>返回数据列表</returns>
        [ActionName("ChildCodeList")]
        public WebApiResult<IQueryable<Basic_DataDictionary>> GetDataDictionaryByCode(string Code)
        {
            return _DataDictionaryService.GetDataDictionaryByCode(Code);
        }
    }
}
