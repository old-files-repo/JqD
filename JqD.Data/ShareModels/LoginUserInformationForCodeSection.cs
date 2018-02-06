namespace JqD.Data.ShareModels
{
    public class LoginUserInformationForCodeSection
    {
        /// <summary>
        /// 登录用户ID
        /// </summary>
        public int SystemUserId { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        public UserInfo UserInfo { get; set; }
    }
}