namespace JqD.Infrustruct.Enums
{
    public class Enums
    {
        /// <summary>
        /// 是否登陆中
        /// </summary>
        public enum IsLogin
        {
            Logining=1,
            UnLogin=0
        }

        /// <summary>
        /// 博客类型
        /// </summary>
        public enum Category
        {
            生活随笔=1,
            Net技术=2,
            Web前段=3,
            数据库技术=4,
            其他随笔=5
        }

        /// <summary>
        /// 存在状态
        /// </summary>
        public enum Status
        {
            Delete=0,
            Exist=1
        }
    }
}
