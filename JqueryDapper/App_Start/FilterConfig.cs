using System.Web.Mvc;

namespace JqueryDapper
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //将Authorize应用到全部的应用程序的范围类，要使AuthorizeAttribute成为全程序的过滤器，
            //只要将其加入全局过滤器集合RegisterGlobalFilters方法
            filters.Add(new AuthorizeAttribute());
        }
    }
}
