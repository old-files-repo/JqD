namespace JqD.Logic
{
    public static class LogicExceptionMessage
    {
        public const string TheSameLoginName = "用户名相同，无效添加";
        public const string LoginNameOrPasswordIsNull = "用户名或密码不能为空";
        public const string LoginNameOrPasswordIsAlphabet = "用户名或密码只能数字和字母";
        public const string LoginNameOrPasswordLengthMoreThanSix = "用户名和密码长度至少6位";
    }
}