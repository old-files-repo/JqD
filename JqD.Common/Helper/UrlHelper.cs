using System.Configuration;

namespace JqD.Common.Helper
{
    public class UrlHelper
    {
        private static readonly string ApplicationDomain = ConfigurationManager.AppSettings["ApplicationDomain"];
        public static string WebUrl()
        {
            return $"http://www{ApplicationDomain}";
        }
    }
}