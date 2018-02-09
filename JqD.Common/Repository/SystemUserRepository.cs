using JqD.Common.Entities;
using JqD.Common.IRepository;
using JqD.Data;
using JqD.Data.Repository;

namespace JqD.Common.Repository
{
    public class SystemUserRepository : RepositoryBase<SystemUser>, ISystemUserRepository
    {
        private static readonly SqlStrings SystemUserSql = new SqlStrings
        {
            TableName = "SystemUser",
            Add =@"INSERT INTO SystemUser(LoginName,Password,LastLoginDate,CreateUser,CreateDate,EditUser,EditDate,IsLogin) 
                VALUES(@LoginName,@Password,@LastLoginDate,@CreateUser,@CreateDate,@EditUser,@EditDate,@IsLogin );",
            Update =
                @"UPDATE SystemUser SET LoginName = @LoginName,Password=@Password,LastLoginDate=@LastLoginDate,
                CreateUser = @CreateUser,CreateDate = @CreateDate,EditUser = @EditUser,EditDate = @EditDate,IsLogin = @IsLogin WHERE Id=@Id ;",
            Delete = @"DELETE FROM SystemUser WHERE Id = @Id ;",
            QueryAll = @"SELECT * FROM SystemUser; ",
            QueryOne = @"SELECT * FROM SystemUser WHERE Id = @Id ;",
            QueryByPage = @"SELECT * FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY Id desc) AS RowNum, * FROM SystemUser)
                          AS RowConstrainedResult WHERE RowNum >= @StartNumber AND RowNum <= @EndNumber;
                          SELECT COUNT(1) FROM SystemUser;"
        };

        public SystemUserRepository(ISqlDatabaseProxy databaseProxy)
            : base(databaseProxy)
        {
            Sql = SystemUserSql;
        }

        protected sealed override SqlStrings Sql
        {
            get { return base.Sql; }
            set { base.Sql = value; }
        }
    }
}