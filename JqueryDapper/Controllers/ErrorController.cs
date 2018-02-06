using System.Web.Mvc;

namespace JqueryDapper.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = "服务器错误，请联系管理员或者检查错误日志！";

            return View();
        }
    }
}