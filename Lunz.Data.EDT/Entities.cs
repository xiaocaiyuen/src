using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunz.Web.Extensions;

namespace Lunz.Data.EDT
{
    #region Application
    partial class Api_Application : IEntity { }
    partial class Api_ApplicationKey : IEntity { }
    partial class Api_ApplicationLog : IEntity { }
    #endregion

    #region Membership
    public interface IMembership_User
    {
        [JsonIgnore]
        ICollection<Membership_AuthToken> Membership_AuthToken { get; set; }
        [JsonIgnore]
        ICollection<Membership_UserResetPassword> Membership_UserResetPassword { get; set; }
    }
    /// <summary>
    /// 用户
    /// </summary>
    partial class Membership_User : IMembership_User, IEntity { }
    /// <summary>
    /// 菜单项
    /// </summary>
    partial class Membership_MenuItem : IEntity { }
    /// <summary>
    /// 重置密码验证表
    /// </summary>
    partial class Membership_UserResetPassword : IEntity { }
    /// <summary>
    /// 用户登录验证表
    /// </summary>
    partial class Membership_AuthToken : IEntity { }
    #endregion

    #region Basic
    partial class Basic_DataDictionary : IEntity, ISortable { }
    partial class Basic_Announcement : IEntity { }
    partial class Object_AttributeCategory : IEntity { }
    partial class Object_AttributeFiled : IEntity { }
    partial class Object_AttributeValue : IEntity { }

    partial class Object_AttributeArea : IEntity { }
    partial class Basic_CompanyInfo : IEntity, ISortable { }
    partial class Basic_BankInfo : IEntity { }
	partial class Basic_ResourceItem: IEntity 
    {
        public virtual string FullUrl
        {
            get { return Url.ToFullUrl(); }
        }
    }
    #endregion

    #region Product
    partial class Product_ProductDefinition : IEntity { }
    partial class Product_ProductContract : IEntity { }
    partial class Product_ProductGpsFee : IEntity { }
    partial class Product_ProductForm : IEntity { }
    #endregion


}
