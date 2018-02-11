using JqD.Data;
using JqD.Data.Repository;
using JqD.Entities;
using JqD.IRepository;

namespace JqD.Repository
{
    public class BlogArticleRepository: RepositoryBase<BlogArticle>, IBlogArticleRepository
    {
        public static SqlStrings UserInfoSql = new SqlStrings
        {
            TableName = "BlogArticle",
            Add = @"INSERT INTO BlogArticle (Title,Category,Content,Remark,CreateUser,
                CreateDate,EditUser,EditDate,Status) VALUES 
                (@Title,@Category,@Content,@Remark,@CreateUser,
                @CreateDate,@EditUser,@EditDate,@Status)",
            Update = @"UPDATE BlogArticle SET Title=@Title,Category=@Category,Content=@Content,
                Remark=@Remark,CreateUser=@CreateUser,CreateDate=@CreateDate,EditUser=@EditUser,
                EditDate=@EditDate,Status=@Status WHERE Id=@Id",
            Delete = @"DELETE FROM BlogArticle WHERE Id=@Id",
            QueryAll = @"SELECT * FROM BlogArticle",
            QueryOne = @"SELECT * FROM BlogArticle WHERE Id=@Id",
            QueryByPage = @"SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY Id desc) AS RowNum,
                          * FROM BlogArticle WHERE Status=1)
                          AS RowConstrainedResult WHERE RowNum >= @StartNumber AND RowNum <= @EndNumber;
                          SELECT COUNT(1) FROM BlogArticle;"
        };

        public BlogArticleRepository(ISqlDatabaseProxy databaseProxy) : base(databaseProxy)
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
