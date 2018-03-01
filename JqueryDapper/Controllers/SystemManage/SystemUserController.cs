using System.Linq;
using System.Web.Mvc;
using JqD.Common.Command.SystemManage;
using JqD.Common.ILogic;
using JqD.Common.Web;
using JqD.Entities.QueryModels;
using JqD.Infrustruct;
using JqueryDapper.ViewModels.SystemManage;

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

        public ActionResult Index(string loginName, int pageNo = 1)
        {
            var startNo = (pageNo - 1) * Pagination.PageNumber + 1;
            var queryParams = new QuerySystemUserModel
            {
                LoginName = loginName,
                StartNo = startNo,
                Rows = Pagination.PageNumber,
                Page = pageNo
            };
            int totleCount;
            var list = _systemUserLogic.QueryByPage(queryParams, out totleCount);
            var model = new SystemUsersPageViewModel
            {
                SystemUsers = list.Select(x => new SystemUserViewModel
                {
                    Id = x.Id,
                    LoginName = x.LoginName,
                    Password = x.Password
                }).ToList(),
                Pagination = new Pagination(startNo, Pagination.PageNumber, totleCount)
            };
            return View("~/Views/SystemManage/SystemUser.cshtml", model);
        }

        [HttpPost]
        public ActionResult Add(AddUserCommand user)
        {
            _systemUserLogic.Add(user);
            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _systemUserLogic.Delete(id);
            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult Update(UpdateUserCommand user)
        {
            _systemUserLogic.Update(user);
            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult Get(int id)
        {
            var result=_systemUserLogic.Get(id);
            return Json(result);
        }
    }
}