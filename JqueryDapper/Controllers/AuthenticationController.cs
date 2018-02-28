using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JqD.Common.ILogic;
using JqD.Common.Web;
using JqueryDapper.ViewModels;

namespace JqueryDapper.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly ISystemUserLogic _systemUserLogic;
        private static readonly string ApplicationDomain = ConfigurationManager.AppSettings["ApplicationDomain"];

        public AuthenticationController(ISystemUserLogic systemUserLogic)
            : base(systemUserLogic)
        {
            _systemUserLogic = systemUserLogic;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginCommand command)
        {
            var loginUser = _systemUserLogic.Login(command.UserName, command.Password);
            FormsAuthenticateUser(loginUser.SystemUserId + "_" + Guid.NewGuid());
            return Json(new { Success = true });
        }

        public ActionResult LogOut(string returnUrl)
        {
            //if (Request.Url != null)
            //{
            //    var aCookie = new HttpCookie("JqDLastVisit")
            //    {
            //        Path = "/",
            //        Value = Request.Url.ToString(),
            //        Expires = DateTime.Now.AddDays(1),
            //        Domain = ApplicationDomain,
            //        HttpOnly = false
            //    };
            //    Response.Cookies.Add(aCookie);
            //}
            FormsAuthentication.SignOut();
            var myCookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                Domain = ApplicationDomain,
                Expires = DateTime.Now.ToUniversalTime().AddDays(-1d)
            };
            Response.Cookies.Add(myCookie);
            _systemUserLogic.LogOut(LoginUser.LoginName);
            return Redirect(JqD.Common.Helper.UrlHelper.WebUrl());
        }

        private void FormsAuthenticateUser(string userId)
        {
            //设置用户的 cookie 的值
            var cookie = FormsAuthentication.GetAuthCookie(userId, false);

            if (string.IsNullOrEmpty(ApplicationDomain))
            {

            }
            cookie.Domain = ApplicationDomain;
            cookie.Expires = DateTime.Now.ToUniversalTime().AddDays(1d);
            Response.Cookies.Add(cookie);
        }
    }
}