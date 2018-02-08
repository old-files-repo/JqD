using System.Web.Mvc;
using JqD.Command.SystemManage;
using JqD.Common.ILogic;
using JqD.Common.Web;

namespace JqueryDapper.Controllers.SystemManage
{
    public class SystemUserController : BaseController
    {
        private readonly ISystemUserLogic _systemUserLogic;

        public SystemUserController(ISystemUserLogic systemUserLogic)
            :base(systemUserLogic)
        {
            _systemUserLogic = systemUserLogic;
        }

        public ActionResult Index()
        {
            return View("~/Views/SystemManage/SystemUser.cshtml");
        }

        [HttpPost]
        public ActionResult Add(AddUserCommand user)
        {
            _systemUserLogic.Insert(user);
            return Json(new { Success = true });
        }
    }
}