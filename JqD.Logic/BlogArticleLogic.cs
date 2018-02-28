using JqD.Command.Work;
using JqD.Common;
using JqD.Common.Logic;
using JqD.Data.CodeSection;
using JqD.Data.Logic;
using JqD.Data.UnitOfWork;
using JqD.Entities;
using JqD.ILogic;
using JqD.Infrustruct.Enums;
using JqD.IRepository;

namespace JqD.Logic
{
    public class BlogArticleLogic : LogicBase<BlogArticle>, IBlogArticleLogic
    {
        private readonly IBlogArticleRepository _articleRepository;
        private readonly ICurrentTimeProvider _currentTimeProvider;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public BlogArticleLogic(IBlogArticleRepository articleRepository,
            ICurrentTimeProvider currentTimeProvider, 
            IUnitOfWorkFactory unitOfWorkFactory) 
            :base(articleRepository)
        {
            _articleRepository = articleRepository;
            _currentTimeProvider = currentTimeProvider;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Add(AddWorkCommand work)
        {
            if (string.IsNullOrEmpty(work.Title) || string.IsNullOrEmpty(work.Content))
            {
                throw new LogicException(LogicExceptionMessage.LoginNameOrPasswordIsNull);
            }
            var info = new BlogArticle
            {
                Title=work.Title,
                Category=work.Category,
                Content=work.Content,
                CreateUser=LoginUserSection.CurrentUser.LoginName,
                CreateDate = _currentTimeProvider.CurrentTime(),
                Status = Enums.Status.Exist
            };
            _articleRepository.Add(info);
        }

        public void LogicDelete(int id)
        {
            using (var unitOfWork= _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var info = _articleRepository.Get(id);
                info.Status = Enums.Status.Delete;
                info.EditUser = LoginUserSection.CurrentUser.LoginName;
                info.EditDate = _currentTimeProvider.CurrentTime();
                _articleRepository.Update(info);
                unitOfWork.Commit();
            }
        }

        public void Update(UpdateWorkCommand work)
        {
            if (string.IsNullOrEmpty(work.Title) || string.IsNullOrEmpty(work.Content))
            {
                throw new LogicException(LogicExceptionMessage.LoginNameOrPasswordIsNull);
            }
            var info = _articleRepository.Get(work.Id);
            info.Title = work.Title;
            info.Category = work.Category;
            info.Content = work.Content;
            info.EditUser = LoginUserSection.CurrentUser.LoginName;
            info.EditDate = _currentTimeProvider.CurrentTime();
            _articleRepository.Update(info);
        }
    }
}
