using MvcApplication2.Service;
using MvcApplication2.Utils;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace MvcApplication2.Filters
{
	/// <summary>
	/// http filter
	/// </summary>
	public class AuthTokenFilter : ActionFilterAttribute
	{

		ICache cache = new RuntimeCache();
		IUserInfoService userInfoService = new DefaultUserInfoService();

		/// <summary>
		/// 方法拦截
		/// </summary>
		/// <param name="filterContext"></param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{

			#region 例外
			bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(
				typeof(AllowAnonymousAttribute), inherit: true)
				 ||
				filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
				typeof(AllowAnonymousAttribute), inherit: true);

			if (skipAuthorization)
				return;
			#endregion

			#region 初始化
			//获取传统context
			var context = filterContext.HttpContext;
			var request = context.Request;//定义传统request对象
										  // 校验码只从Url地址后取，不从表单取
			var coll = new NameValueCollection(request.QueryString);
			coll.Add(request.Form);
			#endregion

			#region token校验
			// 获取token，注意它的前缀约定
			string token = request.Headers[TokenHelper.HEADER_PREFIX] ?? coll.Get(TokenHelper.HEADER_PREFIX);

			// 如果没有传token就返回401，没有授权
			if (token == null)
			{
				filterContext.HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(
					ReponseEntity.Ok(System.Net.HttpStatusCode.Unauthorized, "user Unauthorized 401")));
				filterContext.HttpContext.Response.End();


			}
			else
			{
				// 如果传了token，但是token过期，或者不正确，就返回403，权限不足
				if (!TokenHelper.ValidateToken(token))
				{
					filterContext.HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(
						ReponseEntity.Ok(System.Net.HttpStatusCode.Forbidden, "user Forbidden 403")));
					filterContext.HttpContext.Response.End();

				}

			}
			#endregion

			// 如果token正确，就去渲染对应的方法
			base.OnActionExecuting(filterContext);
		}
	}
}