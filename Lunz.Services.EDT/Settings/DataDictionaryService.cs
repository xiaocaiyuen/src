using Lunz.Data.EDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunz.Data;

namespace Lunz.Services.EDT
{
    /// <summary>
    /// 处理 Basic_DataDictionary 数据实体的 Service 接口
    /// </summary>
    public interface IDataDictionaryService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        WebApiPagingResult<IQueryable<Basic_DataDictionary>> List(PagingOptions paging);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <param name="ParentId">子节点查询条件</param>
        /// <returns>返回数据列表</returns>
        WebApiPagingResult<IQueryable<Basic_DataDictionary>> SubList(PagingOptions paging, Guid? ParentId);
        /// <summary>
        /// 新建数据实体
        /// </summary>
        /// <param name="entity">要新建的数据实体</param>
        /// <returns>返回新建的数据实体</returns>
        WebApiResult<Basic_DataDictionary> Add(Basic_DataDictionary entity);
        /// <summary>
        /// 更新数据实体
        /// </summary>
        /// <param name="entity">要更新的数据实体</param>
        /// <returns>返回更新后的数据实体</returns>
        WebApiResult<Basic_DataDictionary> Update(Basic_DataDictionary entity);
        /// <summary>
        /// 删除数据实体
        /// </summary>
        /// <param name="ids">要删除的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        WebApiResult Delete(Guid[] ids);
        /// <summary>
        /// 撤消删除的数据实体
        /// </summary>
        /// <param name="ids">要撤消删除的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        WebApiResult Restore(Guid[] ids);
        /// <summary>
        /// 上移数据实体
        /// </summary>
        /// <param name="ids">要上移的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        WebApiResult MoveUp(Guid[] ids);
        /// <summary>
        /// 下移数据实体
        /// </summary>
        /// <param name="ids">要下移的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        WebApiResult MoveDown(Guid[] ids);

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        /// <param name="Id">父节点主键Id</param>
        /// <returns>返回数据列表</returns>
        WebApiResult<IQueryable<Basic_DataDictionary>> GetDataDictionaryById(Guid Id);

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        /// <param name="Code">编码Code</param>
        /// <returns>返回数据列表</returns>
        WebApiResult<IQueryable<Basic_DataDictionary>> GetDataDictionaryByCode(string Code);
    }

    /// <summary>
    /// 处理 Basic_DataDictionary 数据实体的 Service 接口
    /// </summary>
    public class DataDictionaryService : ServiceBase, IDataDictionaryService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        public virtual WebApiPagingResult<IQueryable<Basic_DataDictionary>> List(PagingOptions paging)
        {
            var result = new WebApiPagingResult<IQueryable<Basic_DataDictionary>>
            {
                Data = from item in DataContext.Basic_DataDictionary
                       orderby item.SortOrder
                       where item.Deleted == false && item.ParentId == null
                       select item
            };

            return result.AsPaging(paging);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <param name="ParentId">子节点查询条件</param>
        /// <returns>返回数据列表</returns>
        public virtual WebApiPagingResult<IQueryable<Basic_DataDictionary>> SubList(PagingOptions paging, Guid? ParentId)
        {
            var result = new WebApiPagingResult<IQueryable<Basic_DataDictionary>>
            {
                Data = from item in DataContext.Basic_DataDictionary
                       orderby item.SortOrder
                       where item.Deleted == false && item.ParentId == ParentId
                       select item
            };

            return result.AsPaging(paging);
        }
        /// <summary>
        /// 新建数据实体
        /// </summary>
        /// <param name="entity">要新建的数据实体</param>
        /// <returns>返回新建的数据实体</returns>
        public virtual WebApiResult<Basic_DataDictionary> Add(Basic_DataDictionary entity)
        {
            var result = new WebApiResult<Basic_DataDictionary>();

            #region 验证
            if (Validate(result, entity))
            {
                if (DataContext.Basic_DataDictionary.Any(x => x.Deleted == false && x.Name == entity.Name))
                {
                    result.AddError(string.Format("名称 '{0}' 已经存在。", entity.Name));
                }
            }
            #endregion

            #region 保存
            if (result.Success)
            {
                var maxSortOrder = 0;
                var code = entity.Code;
                if (DataContext.Basic_DataDictionary.Any())
                {
                    maxSortOrder = DataContext.Basic_DataDictionary.Max(x => x.SortOrder);
                }
                entity.SortOrder = maxSortOrder + 1;
                entity.CreatedAt = DateTime.Now;
                var ParentId = entity.ParentId;
                if (string.IsNullOrEmpty(entity.Code) && ParentId != null)
                {
                    entity.Code = DataContext.Basic_DataDictionary.FirstOrDefault(x => x.Id == ParentId).Code + entity.SortOrder;
                }

                DataContext.Basic_DataDictionary.Add(entity);

                DataContext.SaveChanges();

                result.Data = entity;
            }
            #endregion

            return result;
        }
        /// <summary>
        /// 更新数据实体
        /// </summary>
        /// <param name="entity">要更新的数据实体</param>
        /// <returns>返回更新后的数据实体</returns>
        public WebApiResult<Basic_DataDictionary> Update(Basic_DataDictionary entity)
        {
            var result = new WebApiResult<Basic_DataDictionary>();

            #region 验证
            if (Validate(result, entity))
            {
                if (DataContext.Basic_DataDictionary.Any(x => x.Deleted == false
                    && x.Id != entity.Id
                    && x.Name == entity.Name))
                {
                    result.AddError(string.Format("名称 '{0}' 已经存在。", entity.Name));
                }
            }
            #endregion

            #region 保存
            if (result.Success)
            {
                var origin = DataContext.Basic_DataDictionary.FirstOrDefault(x => x.Id == entity.Id);
                if (origin != null)
                {
                    origin.Name = entity.Name;
                    origin.Code = entity.Code;
                    origin.StringValue = entity.StringValue;
                    origin.IntValue = entity.IntValue;
                    origin.FloatValue = entity.FloatValue;
                    origin.BoolValue = entity.BoolValue;
                    origin.DateTimeValue = entity.DateTimeValue;
                    origin.UpdatedAt = DateTime.Now;
                    DataContext.SaveChanges();
                    result.Data = origin;
                }
            }
            #endregion

            return result;
        }
        /// <summary>
        /// 删除数据实体
        /// </summary>
        /// <param name="ids">要删除的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        public WebApiResult Delete(Guid[] ids)
        {
            var result = new WebApiResult();

            #region 删除
            var entities = from item in DataContext.Basic_DataDictionary
                           where ids.Contains(item.Id)
                           select item;
            DataContext.Basic_DataDictionary.RemoveRange(entities);
            DataContext.SaveChanges();
            #endregion

            return result;
        }
        /// <summary>
        /// 撤消删除的数据实体
        /// </summary>
        /// <param name="ids">要撤消删除的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        public WebApiResult Restore(Guid[] ids)
        {
            var result = new WebApiResult();
            #region 撤销删除
            DataContext.Basic_DataDictionary.UndoDeleted(ids);
            DataContext.SaveChanges();
            #endregion

            return result;
        }
        /// <summary>
        /// 上移数据实体
        /// </summary>
        /// <param name="ids">要上移的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        public WebApiResult MoveUp(Guid[] ids)
        {
            var result = new WebApiResult();

            if (DataContext.Basic_DataDictionary.MoveUp(ids))
            {
                DataContext.SaveChanges();
            }
            else
            {
                result.AddError("当前已经是第一行。");
            }
            return result;
        }
        /// <summary>
        /// 下移数据实体
        /// </summary>
        /// <param name="ids">要下移的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        public WebApiResult MoveDown(Guid[] ids)
        {
            var result = new WebApiResult();

            if (DataContext.Basic_DataDictionary.MoveDown(ids))
            {
                DataContext.SaveChanges();
            }
            else
            {
                result.AddError("当前已经是最末行。");
            }
            return result;
        }

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        /// <param name="Id">父节点主键Id</param>
        /// <returns>返回数据列表</returns>
        public virtual WebApiResult<IQueryable<Basic_DataDictionary>> GetDataDictionaryById(Guid Id)
        {
            var result = new WebApiResult<IQueryable<Basic_DataDictionary>>
            {
                Data = from item in DataContext.Basic_DataDictionary
                       orderby item.SortOrder
                       where item.Deleted == false && item.ParentId == Id
                       select item
            };

            return result;
        }

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        /// <param name="Code">编码Code</param>
        /// <returns>返回数据列表</returns>
        public virtual WebApiResult<IQueryable<Basic_DataDictionary>> GetDataDictionaryByCode(string Code)
        {
            Guid Id = DataContext.Basic_DataDictionary.FirstOrDefault(p => p.Code == Code).Id;
            var result = new WebApiResult<IQueryable<Basic_DataDictionary>>
            {
                Data = from item in DataContext.Basic_DataDictionary
                       orderby item.SortOrder
                       where item.Deleted == false && item.ParentId == Id
                       select item
            };
            return result;
        }

        /// <summary>
        /// 验证数据实体
        /// </summary>
        /// <param name="result">result 参数</param>
        /// <param name="entity">要验证的数据实体</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        protected bool Validate(WebApiResult<Basic_DataDictionary> result, Basic_DataDictionary entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                result.AddError("请输入参数名称。");
            }

            return result.Success;
        }
    }
}
