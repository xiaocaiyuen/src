using Lunz.Authentication;
using Lunz.Authentication.UCWebService;
using Lunz.Web;
using Lunz.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunz.Authentication.UCWebService;
using Lunz.Data.EDT;

namespace Lunz.Services.EDT
{
    public interface IMembershipService
    {
        //WebApiResult<IQueryable<Membership_MenuItem>> GetMenuItemsByUser(Guid userId);

        //WebApiResult<Membership_MenuItem> GetMenuItemByNgUrl(string ngUrl);

        //WebApiResult<Membership_User> Register(Membership_User user);

        //WebApiResult<Membership_User> UpdateUser(Membership_User user);

        //WebApiResult<Membership_User> GetUserByUsername(string username);

        //WebApiResult<Guid?> Login(string username, string password, bool remember);

        //WebApiResult ChangePassword(string passwork, string newPassword);

        //WebApiResult ForgotPassword(string email);

        //WebApiResult ResetPassword(Guid token, string password);
        WebApiResult<LoginResult> Login(string username, string password, bool remember, Guid? appKey = null);
        /// <summary>
        /// 验证 AuthToken 是否有效
        /// </summary>
        /// <param name="authToken">要验证的登录 token</param>
        /// <returns>有效返回 true, 否则为 false</returns>
        WebApiResult<Membership_AuthToken> ValidateAuthToken(Guid appKey, Guid authToken);
        /// <summary>
        /// 获取当前登陆用户的部分基本信息：名称 电话 邮箱
        /// </summary>
        /// <returns></returns>
        WebApiResult<Lunz.Services.EDT.MembershipService.userbaseinfo> GetUserInfo();
    }

    /// <summary>
    /// 处理与用户相关的 Service
    /// </summary>
    public class MembershipService : ServiceBase, IMembershipService
    {
        protected virtual IAuthenticationServcie AuthService { get; private set; }

        public MembershipService(EDT_DBEntities dataContext, IAuthenticationServcie authService)
        {
            DataContext = dataContext;
            AuthService = authService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名/帐号</param>
        /// <param name="password">密码</param>
        /// <param name="remember">记住登录</param>
        /// <returns>成功返回 true，失败返回错误信息</returns>
        public virtual WebApiResult<LoginResult> Login(string username, string password, bool remember, Guid? appKey = null)
        {
            WebApiResult<LoginResult> result = new WebApiResult<LoginResult>();

            Guid? innerAppKey = AppKey;
            if (innerAppKey == null && appKey != null)
            {
                innerAppKey = appKey;
            }
            try
            {
                var loginUsername = username;

                #region 登录验证
                bool isExist = DataContext.Membership_User
                    .Any(x => (x.Username == username || x.PhoneNumber == username || x.Email == username) && x.Deleted == false);
                Membership_User membershipUser = null;
                if (isExist)
                {
                    membershipUser = DataContext.Membership_User
                        .Where(item => (item.Username == username || item.PhoneNumber == username || item.Email == username) && item.Deleted == false)
                        .FirstOrDefault();

                    if (membershipUser.Actived)//true:禁用 false:启用
                    {
                        result.AddError("当前帐号已被禁用");
                        return result;
                    }

                    loginUsername = membershipUser.Username;//如果membershipuser表中存在记录，则优先使用
                }

                result = AuthService.Login(loginUsername, password);
                if (!result.Success)
                {
                    return result;
                }

                #endregion

                if (!isExist)//membershipUser == null  用户中心存在记录，membershipuser表中不存在
                {
                    membershipUser = new Membership_User
                    {
                        Username = result.Data.User.Username,
                        Password = "!@#$%^&*()",
                        DisplayName = result.Data.User.DisplayName,
                        Email = result.Data.User.Email,
                        Approved = true,
                    };
                    DataContext.Membership_User.Add(membershipUser);
                    DataContext.SaveChanges();
                }

                #region Auth Token
                var applicationKey = DataContext.Api_ApplicationKey.FirstOrDefault(x => x.Id == innerAppKey);
                var token = new Membership_AuthToken
                {
                    Membership_User = membershipUser,
                    Api_ApplicationKey = applicationKey,
                    Expired = DateTime.Now.AddHours(LunzWebConfigurationManager.Membership.AuthTokenTimeout),
                };
                DataContext.Membership_AuthToken.Add(token);
                DataContext.SaveChanges();

                result.Data.Token = token.Id;
                #endregion
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 验证 AuthToken 是否有效
        /// </summary>
        /// <param name="authToken">要验证的登录 token</param>
        /// <returns>有效返回 true, 否则为 false</returns>
        public virtual WebApiResult<Membership_AuthToken> ValidateAuthToken(Guid appKey, Guid authToken)
        {
            var result = new WebApiResult<Membership_AuthToken>();

            var token = DataContext.Membership_AuthToken.FirstOrDefault(x =>
                x.Id == authToken
                && x.Api_ApplicationKey.Id == appKey
                && x.Expired > DateTime.Now);
            if (token == null)
            {
                result.AddError("认证无效或已过期，请重新登录");
            }
            else
            {
                result.Data = token;
            }

            return result;
        }

        /// <summary>
        /// 获取当前登陆用户的部分基本信息：名称 电话 邮箱
        /// </summary>
        /// <returns></returns>
        public WebApiResult<userbaseinfo> GetUserInfo()
        {
            var model = new userbaseinfo();
            model.DisplayName = CurrentUser.DisplayName;
            model.Email = CurrentUser.Email;
            model.PhoneNumber = CurrentUser.PhoneNumber;
            model.UserId = CurrentUser.Id;
            var result = new WebApiResult<userbaseinfo>();
            result.Data = model;
            return result;

        }

        /// <summary>
        /// 返回用户基本信息
        /// </summary>
        public class userbaseinfo
        {
            public Guid UserId { get; set; }
            /// <summary>
            /// 名称
            /// </summary>
            public string DisplayName { get; set; }
            /// <summary>
            /// 电话
            /// </summary>
            public string PhoneNumber { get; set; }
            /// <summary>
            /// Email
            /// </summary>
            public string Email { get; set; }
        }
    }
}
