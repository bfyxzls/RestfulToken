using System.Web;
using System.Web.Mvc;
using MvcApplication2.Filters;

namespace MvcApplication2
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new ExceptionErrorFilter());
			filters.Add(new CorsFilter());
		}
	}
}