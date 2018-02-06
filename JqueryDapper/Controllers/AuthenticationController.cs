using System;
using System.Configuration;
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
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginCommand command)
        {
            var loginUser = _systemUserLogic.Login(command.UserName, command.Password);
            FormsAuthenticateUser(loginUser.SystemUserId + "_" + Guid.NewGuid());
            return command.Password == "12345678" ? Json(new { Success = "IsSimple" }) : Json(new { Success = true });
        }

        private void FormsAuthenticateUser(string userName)
        {
            var cookie = FormsAuthentication.GetAuthCookie(userName, false);

            if (string.IsNullOrEmpty(ApplicationDomain))
            {

            }
            cookie.Domain = ApplicationDomain;
            cookie.Expires = DateTime.Now.ToUniversalTime().AddDays(1d);
            Response.Cookies.Add(cookie);
        }
    }
}