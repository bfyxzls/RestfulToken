using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcApplication2.Filters
{
	/// <summary>
	/// MVC模式下跨域访问
	/// </summary>
	public class CorsFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Dictionary<string, string> headers = new Dictionary<string, string>();

			headers.Add("Access-Control-Allow-Origin", "*");
			headers.Add("Access-Control-Allow-Methods", "*");
			foreach (var item in headers.Keys)
			{
				filterContext.RequestContext.HttpContext.Response.Headers.Add(item, headers[item]);
			}

			base.OnActionExecuting(filterContext);
		}
	}
}
