using System;
using System.Web.Mvc;

namespace MvcApplication2.Filters
{
	public class DIFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Lind.DI.DIFactory.InjectFromObject(filterContext.Controller);
		}
	}
}
