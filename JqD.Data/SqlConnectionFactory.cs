using System.Data;
using System.Data.SqlClient;

namespace JqD.Data
{
    public class SqlConnectionFactory:IDbConnectionFactory
    {
        private readonly IConnectionStringManager _connectionStringManager;
        public SqlConnectionFactory(IConnectionStringManager connectionStringManager)
        {
            _connectionStringManager = connectionStringManager;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionStringManager.ConnectionString);
        }
    }
}
