using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace JqD.Data
{
    public abstract class EntityBaseRepository<T> where T:class
    {
        protected readonly ISqlDatabaseProxy DatabaseProxy;
        protected abstract SqlStrings Sql { get; set; }
        protected abstract Func<string, Dictionary<string, object>, SqlBuilder.Template> QueryByPageSql { get; }

        protected EntityBaseRepository(ISqlDatabaseProxy databaseProxy)
        {
            DatabaseProxy = databaseProxy;
        }

        public int Add(T t)
        {
            if (t == null) throw new ArgumentNullException(typeof(T).Name);
            try
            {
                var count = DatabaseProxy.InsertAndReturnId(Sql.Add, t);

                return count;
            }
            catch (SqlException sqlException)
            {
                throw RepositoryException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw RepositoryException.GeneralError(exception);
            }
        }

        public int AddBatch(IList<T> items)
        {
            if (items == null) throw new ArgumentNullException(typeof(T).Name);
            try
            {
                var count = DatabaseProxy.InsertList(Sql.Add, items);
                return count;
            }
            catch (SqlException sqlException)
            {
                throw RepositoryException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw RepositoryException.GeneralError(exception);
            }
        }

        public int Delete(int id)
        {
            try
            {
                var count = DatabaseProxy.Delete(Sql.Delete, id);
                return count;
            }
            catch (SqlException sqlException)
            {
                throw RepositoryException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw RepositoryException.GeneralError(exception);
            }
        }

        public int Update(T t)
        {
            if (t == null) throw new ArgumentNullException(typeof(T).Name);
            try
            {
                var count = DatabaseProxy.Update(Sql.Update, t);
                return count;
            }
            catch (SqlException sqlException)
            {
                throw RepositoryException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw RepositoryException.GeneralError(exception);
            }
        }

        public T Get(int id)
        {
            try
            {
                return DatabaseProxy.Query<T>(Sql.QueryOne, new { Id = id }).First();
            }
            catch (SqlException sqlException)
            {
                throw RepositoryException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw RepositoryException.GeneralError(exception);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return DatabaseProxy.Query<T>(Sql.QueryAll);
            }
            catch (SqlException sqlException)
            {
                throw RepositoryException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw RepositoryException.GeneralError(exception);
            }
        }

        public IEnumerable<T> QueryByPage(int startNumber, int endNumber, out int totleRecords)
        {
            try
            {
                var items = new List<T>();
                var number = 0;
                DatabaseProxy.QueryMulti(Sql.QueryByPage, new { StartNumber = startNumber, EndNumber = endNumber },
                    reader =>
                    {
                        items = reader.Read<T>().ToList();
                        number = reader.Read<int>().Single();
                        return true;

                    });
                totleRecords = number;
                return items;
            }
            catch (SqlException sqlException)
            {
                throw RepositoryException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw RepositoryException.GeneralError(exception);
            }
        }

        public IEnumerable<T> QueryByPage(int startNumber, int endNumber, 
            out int totleRecords, Dictionary<string, object> querys)
        {
            try
            {
                var items = new List<T>();
                var number = 0;
                var sqlBuild = QueryByPageSql.Invoke(Sql.QueryByPage, querys);
                var parameters = sqlBuild.Parameters;
                parameters.AddDynamicParams(new { StartNumber = startNumber, EndNumber = endNumber });
                DatabaseProxy.QueryMulti(sqlBuild.RawSql, parameters, reader =>
                {
                    items = reader.Read<T>().ToList();
                    number = reader.Read<int>().Single();
                    return true;
                });
                totleRecords = number;
                return items;
            }
            catch (SqlException sqlException)
            {
                throw RepositoryException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw RepositoryException.GeneralError(exception);
            }
        }
    }
}