﻿using System;
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