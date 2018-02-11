using System.Linq;
using System.Web.Mvc;
using JqD.Common.ILogic;
using JqD.Common.Web;
using JqD.ILogic;
using JqD.Infrustruct;
using JqueryDapper.ViewModels.Work;

namespace JqueryDapper.Controllers.Work
{
    public class WorkController : BaseController
    {
        private readonly IBlogArticleLogic _blogArticleLogic;

        public WorkController(ISystemUserLogic systemUserLogic, 
            IBlogArticleLogic blogArticleLogic)
            :base(systemUserLogic)
        {
            _blogArticleLogic = blogArticleLogic;
        }

        public ActionResult Index(int pageNo = 1)
        {
            var startNo = (pageNo - 1) * Pagination.PageNumber + 1;
            int totleCount;
            var list = _blogArticleLogic.QueryByPage(startNo, pageNo * Pagination.PageNumber, out totleCount);
            var model = new WorkPageViewModel
            {
                Works = list.Select(x => new WorkViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Category = x.Category,
                    Remark= x.Remark,
                    CreateUser = x.CreateUser,
                    CreateDate = x.CreateDate.ToString("yyyy-MM-dd")
                }).ToList(),
                Pagination = new Pagination(startNo, Pagination.PageNumber, totleCount),
            };
            return View("~/Views/Work/Work.cshtml", model);
        }

        //[HttpPost]
        //public ActionResult Add(AddUserCommand user)
        //{
        //    _blogArticleLogic.Add(user);
        //    return Json(new { Success = true });
        //}

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    _blogArticleLogic.Delete(id);
        //    return Json(new { Success = true });
        //}

        //[HttpPost]
        //public ActionResult Update(EditUserCommand user)
        //{
        //    _blogArticleLogic.Update(user);
        //    return Json(new { Success = true });
        //}

        //[HttpPost]
        //public ActionResult Get(int id)
        //{
        //    var result= _blogArticleLogic.Get(id);
        //    return Json(result);
        //}
    }
}