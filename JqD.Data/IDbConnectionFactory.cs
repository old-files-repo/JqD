using System.Data;

namespace JqD.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
