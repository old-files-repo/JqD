using System;

namespace JqD.Data.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
    }
}