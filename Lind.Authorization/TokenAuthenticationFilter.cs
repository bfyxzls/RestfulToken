using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lind.Authorization
{
    /// <summary>
    /// 基于token的授权机制
    /// </summary>
    [Lind.DI.Component]
    public class TokenAuthenticationFilter : ActionFilterAttribute
    {
        IUserDetailsAuthenticationProvider userDetailsAuthenticationProvider;
        public TokenAuthenticationFilter(IUserDetailsAuthenticationProvider userDetailsAuthenticationProvider)
        {
            if (userDetailsAuthenticationProvider == null)
            {
                throw new ArgumentException("你需要注入IUserDetailsAuthenticationProvider的实例");
            }
            this.userDetailsAuthenticationProvider = userDetailsAuthenticationProvider;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 例外
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(
                typeof(AllowAttribute), inherit: true)
                 ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                typeof(AllowAttribute), inherit: true);

            if (skipAuthorization)
                return;
            #endregion

            #region 初始化
            //获取传统context
            var context = filterContext.HttpContext;
            //定义传统request对象
            var request = context.Request;
            // 校验码只从Url地址后取，不从表单取
            var coll = new NameValueCollection(request.QueryString);
            coll.Add(request.Form);
            #endregion

            #region token校验
            // 获取token，注意它的前缀约定
            string token = request.Headers[userDetailsAuthenticationProvider.GetHeaderPrefix()]
                                  ?? coll.Get(userDetailsAuthenticationProvider.GetHeaderPrefix());

            // 如果没有传token就返回401，没有授权
            if (token == null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(
                    ReponseEntity.Ok(System.Net.HttpStatusCode.Unauthorized, "user Unauthorized 401")));
                context.Response.End();
            }
            else
            {
                // 如果传了token，但是token过期，或者不正确，就返回403，权限不足
                if (!userDetailsAuthenticationProvider.ValidateAuthentication(token))
                {
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(
                        ReponseEntity.Ok(System.Net.HttpStatusCode.Forbidden, "user Forbidden 403")));
                    context.Response.End();
                }

            }
            #endregion

            base.OnActionExecuting(filterContext);
        }
    }
}
