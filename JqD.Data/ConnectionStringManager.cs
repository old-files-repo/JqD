namespace JqD.Data
{
    public class ConnectionStringManager : IConnectionStringManager
    {
        public ConnectionStringManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString
        {
            get;
            private set;
        }
    }
}