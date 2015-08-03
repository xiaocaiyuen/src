using Lunz.Authentication;
using Lunz.Data.EDT;
using Lunz.Services;
using Lunz.Services.EDT;
using Lunz.Web.EDT.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lunz.Web.EDT.Api.Controllers
{
    public class MembershipController : BaseApiController
    {
        private readonly IMembershipService _membershipServcie;

        public MembershipController(IMembershipService membershipServcie)
        {
            _membershipServcie = membershipServcie;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model">提交信息：账号名(用户名/邮箱地址/手机号)、密码、是否记住登录</param>
        /// <returns>成功返回 true，失败返回错误信息</returns>
        [ActionName("Login")]
        [AllowAnonymous]
        public WebApiResult<LoginResult> PostLogin([FromBody]LoginModel model)
        {
            return _membershipServcie.Login(model.UserName, model.Password, model.Remember);
        }

        /// <summary>
        /// 获取当前登陆用户的部分基本信息：名称 电话 邮箱
        /// </summary>
        /// <returns></returns>
        [ActionName("GetUserInfo")]
        public WebApiResult<MembershipService.userbaseinfo> GetUserInfo()
        {
            return _membershipServcie.GetUserInfo();
        }
    }
}
