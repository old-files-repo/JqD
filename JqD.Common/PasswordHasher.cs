namespace JqD.Common
{
    public class PasswordHasher
    {
        public static bool ValidateHash(string password, string hash)
        {
            return password == hash;
        }
    }
}