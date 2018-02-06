using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace JqD.Data
{
    public class SqlDatabaseProxy: ISqlDatabaseProxy
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public SqlDatabaseProxy(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public int InsertAndReturnId<T>(string sql, T item)
        {
            using (var connection = CreateConnection())
            {
                var newId = connection.Query<int>(sql + ";SELECT CAST(SCOPE_IDENTITY() as int)", item).Single();
                return newId;
            }
        }

        public int InsertList<T>(string sql, IList<T> items)
        {
            using (var connection = CreateConnection())
            {
                return connection.Execute(sql, items);
            }
        }

        public int Delete(string sql, int id)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, new { Id = id });
                return result;
            }
        }

        public int Update<T>(string sql, T item)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, item);
                return result;
            }
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            using (var connection = CreateConnection())
            {
                return connection.Query<T>(sql);
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param)
        {
            using (var connection = CreateConnection())
            {
                return connection.Query<T>(sql, param);
            }
        }

        public T QueryMulti<T>(string sql, object param, Func<SqlMapper.GridReader, T> fill)
        {
            using (var connection = CreateConnection())
            {
                var muti = connection.QueryMultiple(sql, param);
                var t = fill(muti);
                return t;
            }
        }

        private IDbConnection CreateConnection()
        {
            try
            {
                var connection = _dbConnectionFactory.CreateConnection();
                return connection;
            }
            catch (Exception)
            {
                throw new Exception("Failed to create connection");
            }
        }
    }
}