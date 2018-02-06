using JqD.Data;
using JqD.Data.Repository;
using JqD.Entities;
using JqD.IRepository;

namespace JqD.Repository
{
    public class UserInfoRepository: RepositoryBase<UserInfo>, IUserInfoRepository
    {
        public static SqlStrings UserInfoSql = new SqlStrings
        {
            TableName = "UserInfo",
            Add = @"INSERT INTO UserImfo (CheckNo,PassWord,Status,CreateOperator,CreateOperateDate,
                EditOperator,EditOperateDate) VALUES 
                (@CheckNo,@PassWord,@Status,@CreateOperator,@CreateOperateDate,
                @EditOperator,@EditOperateDate)",
            Update = @"UPDATE UserInfo SET CheckNo=@CheckNo,PassWord=@PassWord,Status=@Status,
                CreateOperator=@CreateOperator,CreateOperateDate=@CreateOperateDate,
                EditOperator=@EditOperator,EditOperateDate=@EditOperateDate WHERE Id=@Id",
            Delete = @"DELETE FROM UserInfo WHERE Id=@Id",
            QueryAll = @"SELECT * FROM UserInfo",
            QueryOne = @"SELECT * FROM UserInfo WHERE Id=@Id",
            QueryByPage = @""
        };

        public UserInfoRepository(ISqlDatabaseProxy databaseProxy) : base(databaseProxy)
        {
            Sql = UserInfoSql;
        }

        protected sealed override SqlStrings Sql
        {
            get { return base.Sql; }
            set { base.Sql = value; }
        }
    }
}
