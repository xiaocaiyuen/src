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
    /// 处理 Basic_CompanyInfo 数据实体的 Service 接口
    /// </summary>
    public interface ICompanyInfoService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        WebApiPagingResult<IQueryable<Basic_CompanyInfo>> List(PagingOptions paging);

        /// <summary>
        /// 新建数据实体
        /// </summary>
        /// <param name="entity">要新建的数据实体</param>
        /// <returns>返回新建的数据实体</returns>
        WebApiResult<Basic_CompanyInfo> Add(Basic_CompanyInfo entity);
        /// <summary>
        /// 更新数据实体
        /// </summary>
        /// <param name="entity">要更新的数据实体</param>
        /// <returns>返回更新后的数据实体</returns>
        WebApiResult<Basic_CompanyInfo> Update(Basic_CompanyInfo entity);
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
    }

    /// <summary>
    /// 处理 Basic_CompanyInfo 数据实体的 Service 接口
    /// </summary>
    public class CompanyInfoService : ServiceBase, ICompanyInfoService
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>返回数据列表</returns>
        public virtual WebApiPagingResult<IQueryable<Basic_CompanyInfo>> List(PagingOptions paging)
        {
            var result = new WebApiPagingResult<IQueryable<Basic_CompanyInfo>>
            {
                Data = from item in DataContext.Basic_CompanyInfo
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
        public virtual WebApiResult<Basic_CompanyInfo> Add(Basic_CompanyInfo entity)
        {
            var result = new WebApiResult<Basic_CompanyInfo>();

            #region 验证
            if (Validate(result, entity))
            {
                if (DataContext.Basic_CompanyInfo.Any(x => x.Deleted == false && x.Name == entity.Name))
                {
                    result.AddError(string.Format("名称 '{0}' 已经存在。", entity.Name));
                }
            }
            #endregion

            #region 保存
            if (result.Success)
            {
                var maxSortOrder = 0;
                if (DataContext.Basic_CompanyInfo.Any())
                {
                    maxSortOrder = DataContext.Basic_CompanyInfo.Max(x => x.SortOrder);
                }
                entity.SortOrder = maxSortOrder + 1;
                entity.CreatedAt = DateTime.Now;
                entity.CreatedById = CurrentUserId;
                DataContext.Basic_CompanyInfo.Add(entity);

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
        public WebApiResult<Basic_CompanyInfo> Update(Basic_CompanyInfo entity)
        {
            var result = new WebApiResult<Basic_CompanyInfo>();

            #region 验证
            if (Validate(result, entity))
            {
                if (DataContext.Basic_CompanyInfo.Any(x => x.Deleted == false
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
                var origin = DataContext.Basic_CompanyInfo.FirstOrDefault(x => x.Id == entity.Id);
                if (origin != null)
                {
                    origin.Name = entity.Name;
                    origin.Abbreviation = entity.Abbreviation;
                    origin.Address = entity.Address;
                    origin.BusinessLicense = entity.BusinessLicense;
                    origin.BusinessScope = entity.BusinessScope;
                    origin.BusinessTerm = entity.BusinessTerm;
                    origin.CompanyEmail = entity.CompanyEmail;
                    origin.ContactAddress = entity.ContactAddress;
                    origin.ContactEmail = entity.ContactEmail;
                    origin.ContactIDCard = entity.ContactIDCard;
                    origin.ContactName = entity.ContactName;
                    origin.ContactPhone = entity.ContactPhone;
                    origin.Fax = entity.Fax;
                    origin.Founded = entity.Founded;
                    origin.LegaiEmail = entity.LegaiEmail;
                    origin.LegalAddress = entity.LegalAddress;
                    origin.LegalIDCard = entity.LegalIDCard;
                    origin.LegalName = entity.LegalName;
                    origin.LegalPhone = entity.LegalPhone;
                    origin.LegalPostalCode = entity.LegalPostalCode;
                    origin.LegalTel = entity.LegalTel;
                    origin.NameEN = entity.NameEN;
                    origin.Number = entity.Number;
                    origin.OrganizationCode = entity.OrganizationCode;
                    origin.PostalCode = entity.PostalCode;
                    origin.RegisteredAddress = entity.RegisteredAddress;
                    origin.RegistrationCapital = entity.RegistrationCapital;
                    origin.Remarks = entity.Remarks;
                    origin.TaxRegistrationNo = entity.TaxRegistrationNo;
                    origin.Telephone = entity.Telephone;
                    origin.TypeCode = entity.TypeCode;
                    origin.TypeName = entity.TypeName;
                    origin.UpdatedAt = DateTime.Now;
                    origin.ContactTel = entity.ContactTel;
                    origin.ContactPostalCode = entity.ContactPostalCode;
                    origin.CreatedById = CurrentUserId;
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
            var entities = from item in DataContext.Basic_CompanyInfo
                           where ids.Contains(item.Id)
                           select item;
            DataContext.Basic_CompanyInfo.RemoveRange(entities);
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
            DataContext.Basic_CompanyInfo.UndoDeleted(ids);
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

            if (DataContext.Basic_CompanyInfo.MoveUp(ids))
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

            if (DataContext.Basic_CompanyInfo.MoveDown(ids))
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
        /// 验证数据实体
        /// </summary>
        /// <param name="result">result 参数</param>
        /// <param name="entity">要验证的数据实体</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        protected bool Validate(WebApiResult<Basic_CompanyInfo> result, Basic_CompanyInfo entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                result.AddError("请输入参数名称。");
            }

            return result.Success;
        }
    }
}
