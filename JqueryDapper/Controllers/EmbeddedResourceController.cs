using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using JqD.Common.ILogic;
using JqD.Common.Web;

namespace JqueryDapper.Controllers
{
    public class EmbeddedResourceController : BaseController
    {
        private const string AssemblyNamespace = "JqD.Web.Common";
        private static readonly Dictionary<string, string> MimeTypes = InitializeMimeTypes();

        public EmbeddedResourceController(ISystemUserLogic systemUserLogic)
            : base(systemUserLogic)
        {

        }

        public FileResult Index(string resourcePath, string resourceName)
        {
            var assembly = Assembly.GetAssembly(typeof(AuthenticationController));
            var stream = assembly.GetManifestResourceStream($"{AssemblyNamespace}.{resourcePath}.{resourceName}");
            if (stream == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var contentType = GetContentType(resourceName);

            return File(stream, contentType, resourceName);
        }

        private static string GetContentType(string resourceName)
        {
            var index = resourceName.Substring(resourceName.LastIndexOf('.')).ToLower();
            return MimeTypes[index];
        }

        private static Dictionary<string, string> InitializeMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {
                    ".gif",
                    "image/gif"
                },
                {
                    ".png",
                    "image/png"
                },
                {
                    ".jpg",
                    "image/jpeg"
                },
                {
                    ".js",
                    "text/javascript"
                },
                {
                    ".css",
                    "text/css"
                },
                {
                    ".txt",
                    "text/plain"
                },
                {
                    ".xml",
                    "application/xml"
                },
                {
                    ".zip",
                    "application/zip"
                },
                {
                    ".exe",
                    "files/exe"
                }
            };
        }
    }
}