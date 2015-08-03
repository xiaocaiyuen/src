using Lunz.Data.EDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunz.Services.EDT
{
    public interface IProductContractService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        WebApiPagingResult<IQueryable<Product_ProductContract>> List(PagingOptions paging);
        /// <summary>
        /// 新建数据实体
        /// </summary>
        /// <param name="entity">要新建的数据实体</param>
        /// <returns>返回新建的数据实体</returns>
        WebApiResult<Product_ProductContract> Add(Product_ProductContract entity);
        /// <summary>
        /// 更新数据实体
        /// </summary>
        /// <param name="entity">要更新的数据实体</param>
        /// <returns>返回更新后的数据实体</returns>
        WebApiResult<Product_ProductContract> Update(Product_ProductContract entity);
        /// <summary>
        /// 查询数据实体
        /// </summary>
        /// <param name="id">要查询数据参数</param>
        /// <returns>返回查询后的数据实体</returns>
        WebApiResult<Product_ProductContract> SelectInfoById(Guid id);
        /// <summary>
        /// 删除数据实体
        /// </summary>
        /// <param name="ids">要删除的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        WebApiResult Delete(Guid[] ids);
    }

    public class ProductContractService : ServiceBase, IProductContractService
    {

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        public virtual WebApiPagingResult<IQueryable<Product_ProductContract>> List(PagingOptions paging)
        {
            var result = new WebApiPagingResult<IQueryable<Product_ProductContract>>
            {
                Data = from item in DataContext.Product_ProductContract
                       orderby item.SortOrder
                       where item.Deleted == false
                       select item
            };
            return result.AsPaging(paging);
        }
        /// <summary>
        /// 新建数据实体
        /// </summary>
        /// <param name="entity">要新建的数据实体</param>
        /// <returns>返回新建的数据实体</returns>
        public WebApiResult<Product_ProductContract> Add(Product_ProductContract entity)
        {
            var result = new WebApiResult<Product_ProductContract>();

            #region 验证
            if (Validate(result, entity))
            {
                if (DataContext.Product_ProductContract.Any(x => x.Deleted == false && x.Name == entity.Name))
                {
                    result.AddError(string.Format("名称 '{0}' 已经存在。", entity.Name));
                }
            }
            #endregion

            #region 保存
            if (result.Success)
            {
                var maxSortOrder = 0;
                if (DataContext.Product_ProductContract.Any())
                {

                    maxSortOrder = DataContext.Product_ProductContract.Max(x => x.SortOrder);
                }
                entity.SortOrder = maxSortOrder + 1;
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                // entity.Deleted = false;
                DataContext.Product_ProductContract.Add(entity);

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
        public virtual WebApiResult<Product_ProductContract> Update(Product_ProductContract entity)
        {
            var result = new WebApiResult<Product_ProductContract>();

            #region 验证
            if (Validate(result, entity))
            {
                if (DataContext.Product_ProductContract.Any(x => x.Deleted == false
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
                var origin = DataContext.Product_ProductContract.FirstOrDefault(x => x.Id == entity.Id);
                if (origin != null)
                {
                    origin.Name = entity.Name;
                    origin.TemplateHtml = entity.TemplateHtml;
                    entity.UpdatedAt = DateTime.Now;
                    DataContext.SaveChanges();

                    result.Data = origin;
                }
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 查询数据实体
        /// </summary>
        /// <param name="id">要查询数据参数</param>
        /// <returns>返回查询后的数据实体</returns>
        public virtual WebApiResult<Product_ProductContract> SelectInfoById(Guid id)
        {
            var result = new WebApiResult<Product_ProductContract>();
            if (id != null)
            {
                var origin = DataContext.Product_ProductContract.FirstOrDefault(x => x.Id == id);
                result.Data = origin;
            }
            return result;
        }
        /// <summary>
        /// 验证数据实体
        /// </summary>
        /// <param name="result">result 参数</param>
        /// <param name="entity">要验证的数据实体</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        protected bool Validate(WebApiResult<Product_ProductContract> result, Product_ProductContract entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                result.AddError("请输入参数合同名称。");
            }
            if (string.IsNullOrWhiteSpace(entity.TemplateHtml.ToString()))
            {
                result.AddError("请输入参数合同内容。");
            }
            return result.Success;
        }
        /// <summary>
        /// 删除数据实体
        /// </summary>
        /// <param name="ids">要删除的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        public virtual WebApiResult Delete(Guid[] ids)
        {
            var result = new WebApiResult();

            #region 删除
            var entities = from item in DataContext.Product_ProductContract
                           where ids.Contains(item.Id)
                           select item;
            DataContext.Product_ProductContract.RemoveRange(entities);
            DataContext.SaveChanges();
            #endregion

            return result;
        }
    }
}
