using System;
using System.Collections.Generic;

namespace JqD.Data.Repository
{
    public abstract class RepositoryBase<T> : EntityBaseRepository<T>, IRepositoryBase<T> where T : class
    {
        protected override SqlStrings Sql { get; set; }
        protected override Func<string, Dictionary<string, object>, SqlBuilder.Template> QueryByPageSql => GenerateQueryByPageSql;
        protected RepositoryBase(ISqlDatabaseProxy databaseProxy):base(databaseProxy)
        {
            
        }

        protected virtual SqlBuilder.Template GenerateQueryByPageSql(string queryByPageTemplate, Dictionary<string, object> querys)
        {
            //You have to override this, otherwise it will throw exception
            throw new NotImplementedException();
        }
    }
}