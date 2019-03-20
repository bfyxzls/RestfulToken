using System;
using System.Web.Http.Filters;
using System.Web.Mvc;
using MvcApplication2.Utils;

namespace MvcApplication2.Filters
{
    /// <summary>
    /// 异常拦截器
    /// </summary>
	public class ExceptionErrorFilter : HandleErrorAttribute
	{
		#region IExceptionFilter 成员

		public override void OnException(ExceptionContext filterContext)
		{
			filterContext.HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(
				ReponseEntity.ThrowError(filterContext.Exception)));
			filterContext.HttpContext.Response.End();
		}

		#endregion
	}
}
