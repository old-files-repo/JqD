using System;
using System.Data.SqlClient;

namespace JqD.Data
{
    public class RepositoryException : Exception
    {
        public RepositoryException(Exception exception, string message) : base(message, exception)
        {

        }

        public static RepositoryException DatabaseError(SqlException sqlException)
        {
            return new RepositoryException(sqlException, "Error connecting to the database");
        }

        public static RepositoryException GeneralError(Exception exception)
        {
            return new RepositoryException(exception, "General failure in repository");
        }
    }
}