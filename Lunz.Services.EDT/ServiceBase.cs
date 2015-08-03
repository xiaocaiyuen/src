using Lunz.Data.EDT;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunz.Services.EDT
{
    /// <summary>
    /// Service基类
    /// </summary>
    /// <remarks>
    /// 所有Service类必须从继承 ServiceBase 类。
    /// </remarks>
    public abstract class ServiceBase : ServiceBase<EDT_DBEntities>
    {
        /// <summary>
        /// 获取或设置EF数据实体
        /// </summary>
        [Dependency]
        public override EDT_DBEntities DataContext
        {
            get
            {
                return base.DataContext;
            }
            set
            {
                base.DataContext = value;
            }
        }
        /// <summary>
        /// 当前登录用户
        /// </summary>
        private Membership_User _currentUser;
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        public virtual Membership_User CurrentUser
        {
            get
            {
                if (IsAuthenticated && _currentUser == null)
                {
                    _currentUser = DataContext.Membership_User
                        .FirstOrDefault(x => x.Id == CurrentUserId);
                }
                return _currentUser;
            }
        }
        private Guid? _appKey;
        /// <summary>
        /// 获取 AppKey
        /// </summary>
        public virtual Guid? AppKey
        {
            get
            {
                if (_appKey == null)
                {
                    var appKeyString = WebContext.Request.QueryString["appKey"];
                    Guid appKey;
                    if (Guid.TryParse(appKeyString, out appKey))
                    {
                        _appKey = appKey;
                    }
                }
                return _appKey;
            }
        }
    }
}
