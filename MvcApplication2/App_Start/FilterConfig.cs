using System.Web;
using System.Web.Mvc;
using MvcApplication2.Filters;

namespace MvcApplication2
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
			//从ioc容器里拿到 filter实例 
			filters.Add(new DIFilter());
           // filters.Add(DependencyResolver.Current.GetService<Lind.Authorization.TokenAuthenticationFilter>());
            filters.Add(new ExceptionErrorFilter());
			filters.Add(new CorsFilter());
		}
	}
}