using JqD.Command.Work;
using JqD.Data.Logic;
using JqD.Entities;

namespace JqD.ILogic
{
    public interface IBlogArticleLogic : ILogicBase<BlogArticle>
    {
        void Add(AddWorkCommand work);

        void LogicDelete(int id);

        void Update(UpdateWorkCommand work);
    }
}
