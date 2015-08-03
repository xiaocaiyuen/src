using Lunz.Data.EDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunz.Services.EDT
{
    public interface IProductDefinitionService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        WebApiPagingResult<IQueryable<Product_ProductDefinition>> List(PagingOptions paging);
        /// <summary>
        /// 新建数据实体
        /// </summary>
        /// <param name="entity">要新建的数据实体</param>
        /// <returns>返回新建的数据实体</returns>
        WebApiResult<Product_ProductDefinition> Add(Product_ProductDefinition entity);
        /// <summary>
        /// 更新数据实体
        /// </summary>
        /// <param name="entity">要更新的数据实体</param>
        /// <returns>返回更新后的数据实体</returns>
        WebApiResult<Product_ProductDefinition> Update(Product_ProductDefinition entity);
        /// <summary>
        /// 删除数据实体
        /// </summary>
        /// <param name="ids">要删除的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        WebApiResult Delete(Guid[] ids);
    }
    public class ProductDefinitionService : ServiceBase, IProductDefinitionService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        public virtual WebApiPagingResult<IQueryable<Product_ProductDefinition>> List(PagingOptions paging)
        {
            var result = new WebApiPagingResult<IQueryable<Product_ProductDefinition>>
            {
                Data = from item in DataContext.Product_ProductDefinition
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
        public virtual WebApiResult<Product_ProductDefinition> Add(Product_ProductDefinition entity)
        {
            var result = new WebApiResult<Product_ProductDefinition>();

            #region 验证
            if (Validate(result, entity))
            {
                if (DataContext.Product_ProductDefinition.Any(x => x.Deleted == false && x.Name == entity.Name))
                {
                    result.AddError(string.Format("名称 '{0}' 已经存在。", entity.Name));
                }
            }
            #endregion

            #region 保存
            if (result.Success)
            {
                var maxSortOrder = 0;
                if (DataContext.Product_ProductDefinition.Any())
                {
                    maxSortOrder = DataContext.Product_ProductDefinition.Max(x => x.SortOrder);
                }
                entity.SortOrder = maxSortOrder + 1;
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                DataContext.Product_ProductDefinition.Add(entity);

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
        public virtual WebApiResult<Product_ProductDefinition> Update(Product_ProductDefinition entity)
        {
            var result = new WebApiResult<Product_ProductDefinition>();

            #region 验证
            if (Validate(result, entity))
            {
                if (DataContext.Product_ProductDefinition.Any(x => x.Deleted == false
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
                var origin = DataContext.Product_ProductDefinition.FirstOrDefault(x => x.Id == entity.Id);
                if (origin != null)
                {
                    origin.Name = entity.Name;
                    origin.DownPayment = entity.DownPayment;
                    origin.Margin = entity.Margin;
                    origin.Fee = entity.Fee;
                    origin.FinalPayment = entity.FinalPayment;
                    origin.InterestRate = entity.InterestRate;
                    origin.Lease = entity.Lease;
                    //origin.CalType = entity.CalType;
                    entity.UpdatedAt = DateTime.Now;
                    DataContext.SaveChanges();

                    result.Data = origin;
                }
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 验证数据实体
        /// </summary>
        /// <param name="result">result 参数</param>
        /// <param name="entity">要验证的数据实体</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        protected bool Validate(WebApiResult<Product_ProductDefinition> result, Product_ProductDefinition entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                result.AddError("请输入参数产品名称。");
            }
            //if (string.IsNullOrWhiteSpace(entity.CalType.ToString()))
            //{
            //    result.AddError("请输入参数产品计算类型。");
            //}
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
            var entities = from item in DataContext.Product_ProductDefinition
                           where ids.Contains(item.Id)
                           select item;
            DataContext.Product_ProductDefinition.RemoveRange(entities);
            DataContext.SaveChanges();
            #endregion

            return result;
        }
        /// <summary>
        /// 撤消删除的数据实体
        /// </summary>
        /// <param name="ids">要撤消删除的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        public virtual WebApiResult Restore(Guid[] ids)
        {
            var result = new WebApiResult();

            #region 撤销删除
            //DataContext.Vehicle_CHH.UndoDeleted(ids);
            DataContext.SaveChanges();
            #endregion

            return result;
        }
    }
}
