using MvcApplication2.Service;
using MvcApplication2.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

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
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            #region 例外
            // 登陆页面控制器上添加AllowAnonymousAttribute，表示可以不带token就可以访问
            bool skipAuthorization = actionContext.ControllerContext.ControllerDescriptor.ControllerType.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) ||
           actionContext.ControllerContext.ControllerDescriptor.ControllerType.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
                return;
            #endregion

            #region 初始化
            //获取传统context
            var context = (HttpContextBase)actionContext.Request.Properties["MS_HttpContext"];
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
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        Status = 0,
                        ErrorCode = "401",
                        ErrorMessage = "not find token"
                    }))
                };

            }
            else
            {
                // 如果传了token，但是token过期，或者不正确，就返回403，权限不足
                if (!TokenHelper.ValidateToken(token))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            Status = 0,
                            ErrorCode = "403",
                            ErrorMessage = "token not validate"
                        }))
                    };

                }

            }
            #endregion

            // 如果token正确，就去渲染对应的方法
            base.OnActionExecuting(actionContext);
        }
    }
}