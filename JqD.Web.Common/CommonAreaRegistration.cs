using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace JqD.Web.Common
{
    public class CommonAreaRegistration : PortableAreaRegistration
    {
        public override string AreaName => "Common";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                AreaName,
                AreaName + "/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "JqD.Web.Common.Controllers" }
            );
            base.RegisterArea(context);
        }
    }
}