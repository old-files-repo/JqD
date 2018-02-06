using System;
using System.Collections.Generic;
using Dapper;

namespace JqD.Data
{
    public interface ISqlDatabaseProxy
    {

        int InsertAndReturnId<T>(string sql, T item);

        int InsertList<T>(string sql, IList<T> items);

        int Delete(string sql, int id);

        int Update<T>(string sql, T item);

        IEnumerable<T> Query<T>(string sql);

        IEnumerable<T> Query<T>(string sql, object param);

        T QueryMulti<T>(string sql, object param, Func<SqlMapper.GridReader, T> fill);
    }
}