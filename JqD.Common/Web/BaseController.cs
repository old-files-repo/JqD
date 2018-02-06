using System.Web.Mvc;
using JqD.Common.Helper;
using JqD.Common.ILogic;
using JqD.Common.Models;
using JqD.Data.CodeSection;
using JqD.Data.ShareModels;
using JqD.Infrustruct;

namespace JqD.Common.Web
{
    public class BaseController: Controller
    {
        private LoginUserInformation _loginUserInformation;
        private readonly ISystemUserLogic _systemUserLogic;
        private LoginUserSection _loginUserSection;

        public BaseController(ISystemUserLogic systemUserLogic)
        {
            _systemUserLogic = systemUserLogic;
        }

        protected LoginUserInformation LoginUser
        {
            get
            {
                if (_loginUserInformation != null) return _loginUserInformation;
                var loginUserId = System.Web.HttpContext.Current == null
                    ? string.Empty
                    : System.Web.HttpContext.Current.User.Identity.Name;

                if (string.IsNullOrEmpty(loginUserId))
                {
                    return null;
                }

                //判断Cache中是否有当前用户
                if (CacheManager.Contains(loginUserId))
                {
                    _loginUserInformation = CacheManager.Get<LoginUserInformation>(loginUserId);
                }
                else // : 无 
                {
                    if (loginUserId.Split('_').Length != 2)
                    {
                        return null;
                    }
                    _loginUserInformation = _systemUserLogic.GetLoginUserInformationById(int.Parse(loginUserId.Split('_')[0]));
                    CacheManager.Add(loginUserId, _loginUserInformation);
                }
                return _loginUserInformation;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (LoginUser == null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                //var actionName = filterContext.ActionDescriptor.ActionName;
                //var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                //var roleFunctions = LoginUser.RoleAndFunctions.Where(x => x.ControllerName == controllerName);
                _loginUserSection = LoginUserSection.Start(new LoginUserInformationForCodeSection
                {
                    SystemUserId = LoginUser.SystemUserId,
                    LoginName = LoginUser.LoginName,
                    UserInfo = new UserInfo
                    {
                        UserId = LoginUser.UserInfo.UserId,
                        UserNo = LoginUser.UserInfo.UserNo
                    }
                });
                base.OnActionExecuting(filterContext);

            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _loginUserSection?.Dispose();
            base.OnActionExecuted(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            LogHelper.WriteLog("Service error", filterContext.Exception);
            filterContext.ExceptionHandled = true;
            if (Request.IsAjaxRequest())
            {
                Response.StatusCode = 500;
                Response.TrySkipIisCustomErrors = true;
                filterContext.Result = Json(new { Result = false, filterContext.Exception.Message });
            }
            else
            {
                // ReSharper disable once Mvc.AreaNotResolved
                filterContext.Result = RedirectToAction("Index", "Error", new { Area = "Common" });
            }
        }
    }
}
