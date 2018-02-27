using System;
using System.Linq;
using System.Web.Mvc;
using JqD.ILogic;
using JqueryDapper.ViewModels;

namespace JqueryDapper.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogArticleLogic _blogArticleLogic;
        private const string ImagePath = "/images/LoadImages/";

        public HomeController(IBlogArticleLogic blogArticleLogic)
        {
            _blogArticleLogic = blogArticleLogic;
        }

        // GET: Home
        public ActionResult Index()
        {
            var allArticleList = _blogArticleLogic.GetAll().OrderByDescending(x=>x.CreateDate).ToList();
            var model = new HomePageViewModel
            {
                Homes = allArticleList.Select(x => new HomeViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Category = x.Category,
                    ImagePathStatus= x.Content.Contains(ImagePath),
                    ImagePath = GetContentImagePath(x.Content),
                    CreateUser = x.CreateUser,
                    CreateDate = x.CreateDate.ToString("yyyy-MM-dd")
                }).ToList()
            };
            return View(model);
        }

        private string GetContentImagePath(string content)
        {
            var result = "";
            if (!content.Contains(ImagePath)) return result;
            var indexofA = content.IndexOf(ImagePath, StringComparison.Ordinal);
            result = content.Substring(indexofA + 19);
            var indexofB = result.IndexOf(">", StringComparison.Ordinal);
            result = result.Substring(0, indexofB- 1);
            return ImagePath+result;
        }
    }
}