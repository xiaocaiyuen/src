using Lunz.Services;
using Lunz.Services.EDT;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Lunz.Authentication;

namespace Lunz.Web.EDT.Management.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        private IMembershipService _membershipService;

        public LoginController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password, bool rememberMe, string returnUrl)
        {
            var appKeyStr = System.Configuration.ConfigurationManager.AppSettings["AppKey"];
            var appKey = Guid.Parse(appKeyStr);

            var result = _membershipService.Login(username, password, rememberMe, appKey);
            if (result.Success)
            {
                var authToken = result.Data.Token.ToString();
                Response.Cookies.Add(new HttpCookie("AUTHTOKEN", authToken));

                Session["current-user"] = result.Data;
                FormsAuthentication.SetAuthCookie(authToken, false);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult GetUser()
        {
            var result = new WebApiResult<LoginResult>();
            if (Session["current-user"] is LoginResult)
            {
                result.Data = Session["current-user"] as LoginResult;
            }
            else
            {
                result.AddError("当前用户未登录。");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Response.Cookies.Remove("AUTHTOKEN");

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Login");
        }

    }
}