using Lunz.Data.EDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunz.Services.EDT
{
    /// <summary>
    /// 处理 BankInfo 数据实体的 Service 接口
    /// </summary>
    public interface IBankInfoService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        WebApiPagingResult<IQueryable<Basic_BankInfo>> List(PagingOptions paging);
        /// <summary>
        /// 新建数据实体
        /// </summary>
        /// <param name="entity">要新建的数据实体</param>
        /// <returns>返回新建的数据实体</returns>
        WebApiResult<Basic_BankInfo> Add(Basic_BankInfo entity);
        /// <summary>
        /// 更新数据实体
        /// </summary>
        /// <param name="entity">要更新的数据实体</param>
        /// <returns>返回更新后的数据实体</returns>
        WebApiResult<Basic_BankInfo> Update(Basic_BankInfo entity);
        /// <summary>
        /// 删除数据实体
        /// </summary>
        /// <param name="ids">要删除的数据实体主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        WebApiResult Delete(Guid[] ids);
    }
    public class BankInfoService:ServiceBase,IBankInfoService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        public WebApiPagingResult<IQueryable<Basic_BankInfo>> List(PagingOptions paging)
        {
            var result = new WebApiPagingResult<IQueryable<Basic_BankInfo>>
            {
                Data = from item in DataContext.Basic_BankInfo
                       where item.Deleted == false
                       orderby item.CreatedAt descending
                       select item
            };

            return result.AsPaging(paging);
        }

        /// <summary>
        /// 新建数据实体
        /// </summary>
        /// <param name="entity">要新建的数据实体</param>
        /// <returns>返回新建的数据实体</returns>
        public WebApiResult<Basic_BankInfo> Add(Basic_BankInfo entity)
        {
            var result = new WebApiResult<Basic_BankInfo>();

            #region 验证
            if (Validate(result, entity))
            {
                if (DataContext.Basic_BankInfo.Any(x => x.Deleted == false && x.Name == entity.Name))
                {
                    result.AddError(string.Format("开户行全称 '{0}' 已经存在。", entity.Name));
                }
            }
            #endregion

            #region 保存
            if (result.Success)
            {
                DataContext.Basic_BankInfo.Add(entity);
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
        public virtual WebApiResult<Basic_BankInfo> Update(Basic_BankInfo entity)
        {
            var result = new WebApiResult<Basic_BankInfo>();

            #region 验证
            if (Validate(result, entity))
            {
                if (DataContext.Basic_BankInfo.Any(x => x.Deleted == false && x.Id != entity.Id && x.Name == entity.Name))
                {
                    result.AddError(string.Format("开户行全称 '{0}' 已经存在。", entity.Name));
                }
            }
            #endregion

            #region 保存
            if (result.Success)
            {
                var origin = DataContext.Basic_BankInfo.FirstOrDefault(x => x.Id == entity.Id);
                if (origin != null)
                {
                    origin.Name = entity.Name;
                    origin.TypeId = entity.TypeId;
                    origin.ContactPerson = entity.ContactPerson;
                    origin.Telephone = entity.Telephone;
                    origin.PostalCode = entity.PostalCode;
                    origin.Address = entity.Address;
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
        protected bool Validate(WebApiResult<Basic_BankInfo> result, Basic_BankInfo entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                result.AddError("请输入参数开户行全称。");
            }
            if (string.IsNullOrWhiteSpace(entity.Name.ToString()))
            {
                result.AddError("请输入参数开户行全称。");
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
            var entities = from item in DataContext.Basic_BankInfo
                           where ids.Contains(item.Id)
                           select item;
            DataContext.Basic_BankInfo.RemoveRange(entities);
            DataContext.SaveChanges();
            #endregion

            return result;
        }
    }
}
