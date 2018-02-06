using System.Collections.Generic;

namespace JqD.Data.Logic
{
    public interface ILogicBase<T>
    {
        int Add(T model);

        int AddBatch(IList<T> items);

        int Delete(int id);

        int Update(T model);

        T Get(int id);

        IEnumerable<T> GetAll();
    }
}