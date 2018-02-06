using JqD.Common.Entities;
using JqD.Common.IRepository;
using JqD.Data;
using JqD.Data.Repository;

namespace JqD.Common.Repository
{
    public class OperationLogsRepository : RepositoryBase<OperationLogs>, IOperationLogsRepository
    {
        public static SqlStrings OperationLogsSql = new SqlStrings
        {
            TableName = "OperationLogs",
            Add = @"insert into [OperationLogs] (Operation,OperatorId,ModelType,ModelId,ModelJson,OperationTime) "
                  + " values (@Operation,@OperatorId,@ModelType,@ModelId,@ModelJson,@OperationTime)",
            QueryAll = @"SELECT * FROM [OperationLogs]",
            QueryOne = @"SELECT * FROM [OperationLogs] WHERE Id = @Id ;",
            QueryByPage =
                @"SELECT * FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY Id DESC) AS RowNum, * FROM [OperationLogs]) AS RowConstrainedResult "
                + "WHERE RowNum BETWEEN @StartNumber AND @EndNumber;"
                + "SELECT COUNT(1) FROM [OperationLogs];"
        };

        public OperationLogsRepository(ISqlDatabaseProxy databaseProxy)
            : base(databaseProxy)
        {
            Sql = OperationLogsSql;
        }

        protected sealed override SqlStrings Sql
        {
            get { return base.Sql; }
            set { base.Sql = value; }
        }
    }
}