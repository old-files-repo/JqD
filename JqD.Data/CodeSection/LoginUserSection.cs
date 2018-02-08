using JqD.Data.ShareModels;

namespace JqD.Data.CodeSection
{
    /// <summary>
    /// 存登录用户，在后台记录日志
    /// </summary>
    public class LoginUserSection : BaseCodeSection
    {
        private LoginUserSection(LoginUserInformationForCodeSection systemUser)
        {
            currentUser = systemUser;
            BeginSection();
        }

        // ReSharper disable once InconsistentNaming
        private LoginUserInformationForCodeSection currentUser { get; set; }

        public static LoginUserSection Start(LoginUserInformationForCodeSection systemUser)
        {
            var section = new LoginUserSection(systemUser);
            return section;
        }

        public static bool IsInSection => IsThreadInSection<LoginUserSection>();

        public static LoginUserInformationForCodeSection CurrentUser
        {
            get
            {
                if (!IsInSection) return null;
                var root = GetSectionRoot<LoginUserSection>();
                return root.currentUser;
            }
        }
    }
}