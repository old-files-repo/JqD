using JqD.Data.Logic;
using JqD.Entities;
using JqD.ILogic;
using JqD.IRepository;

namespace JqD.Logic
{
    public class BlogArticleLogic : LogicBase<BlogArticle>, IBlogArticleLogic
    {
        private readonly IBlogArticleRepository _articleRepository;

        public BlogArticleLogic(IBlogArticleRepository articleRepository) :base(articleRepository)
        {
            _articleRepository = articleRepository;
        }

    }
}
