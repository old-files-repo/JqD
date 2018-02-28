using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using JqD.Data.CodeSection;
using JqD.Data.ShareModels;
using Newtonsoft.Json;

namespace JqD.Data
{
    internal class SqlDatabaseProxy: ISqlDatabaseProxy
    {
        private const string AddOperationLogs = @"INSERT INTO OperationLogs (Operation,OperatorId,ModelType,ModelId,ModelJson,OperationTime,OperationSQL)
                                             VALUES (@Operation,@OperatorId,@ModelType,@ModelId,@ModelJson,@OperationTime,@OperationSQL)";

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
                InsertOperationLog(sql, "添加", item);
                return newId;
            }
        }

        public int InsertList<T>(string sql, IList<T> items)
        {
            using (var connection = CreateConnection())
            {
                var count=connection.Execute(sql, items);
                InsertOperationLog(sql, "批量添加", items);
                return count;
            }
        }

        public int Delete(string sql, int id)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, new { Id = id });
                InsertOperationLog(sql, "删除", id);
                return result;
            }
        }

        public int Update<T>(string sql, T item)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, item);
                InsertOperationLog(sql, "修改", item);
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

        private void InsertOperationLog(string sql, string operation, int id)
        {
            var log = new OperationLogs
            {
                ModelId = id,
                ModelJson = "",
                ModelType = "",
                Operation = operation,
                OperatorId = LoginUserSection.CurrentUser == null ? "" : LoginUserSection.CurrentUser.SystemUserId.ToString(),
                OperationSQL = sql,
                OperationTime = DateTime.Now
            };
            using (var connection = CreateConnection())
            {
                connection.Execute(AddOperationLogs, log);
            }
        }

        private void InsertOperationLog<T>(string sql, string operation, params T[] items)
        {
            var log = new OperationLogs
            {
                ModelId = 0,
                ModelJson = JsonConvert.SerializeObject(items),
                ModelType = "",
                Operation = LoginUserSection.CurrentUser == null ? "登录" : operation,
                OperatorId = LoginUserSection.CurrentUser == null ? "" : LoginUserSection.CurrentUser.SystemUserId.ToString(),
                OperationSQL = sql,
                OperationTime = DateTime.Now
            };
            using (var connection = CreateConnection())
            {
                connection.Execute(AddOperationLogs, log);
            }
        }

    }
}