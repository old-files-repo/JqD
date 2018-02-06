using System.Configuration;

namespace JqD.Data
{
    public class DataConfig
    {
        private static string GetConnectionStringKey()
        {
            return "JqD";
        }

        public static string GetConnectionString()
        {
            var connectionStringKey = GetConnectionStringKey();

            if (ConfigurationManager.ConnectionStrings[connectionStringKey] == null)
                throw new ConfigurationErrorsException(
                    "Missing connectionstring for key '" + connectionStringKey + "', add one to the web.config");
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
            return connectionString;
        }
    }
}