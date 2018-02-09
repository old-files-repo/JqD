using System.Collections.Generic;
using JqD.Data.Repository;

namespace JqD.Data.Logic
{
    public abstract class LogicBase<T>
    {
        private readonly IRepositoryBase<T> _registerApplyRepository;

        protected LogicBase(IRepositoryBase<T> registerApplyRepository)
        {
            _registerApplyRepository = registerApplyRepository;
        }

        public int Add(T model)
        {
            return _registerApplyRepository.Add(model);
        }

        public int AddBatch(IList<T> items)
        {
            return _registerApplyRepository.AddBatch(items);
        }

        public int Delete(int id)
        {
            return _registerApplyRepository.Delete(id);
        }

        public int Update(T model)
        {
            var result = _registerApplyRepository.Update(model);
            return result;
        }

        public T Get(int id)
        {
            return _registerApplyRepository.Get(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _registerApplyRepository.GetAll();
        }

        public IEnumerable<T> QueryByPage(int startNumber, int endNumber, out int totleRecords)
        {
            return _registerApplyRepository.QueryByPage(startNumber, endNumber, out totleRecords);
        }
    }
}