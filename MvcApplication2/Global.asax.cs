using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using Autofac.Integration.Mvc;
using Lind.Authorization;
using Lind.DDD.Caching;
using MvcApplication2.DITest;
using MvcApplication2.Models;
using MvcApplication2.Service;
using MvcApplication2.Services;

namespace MvcApplication2
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            #region 注册组件
            var builder = new ContainerBuilder();
            builder.RegisterType<UserInfo>().As<IUserDetails>();
            builder.RegisterType<UserService>().As<IUserDetailsService>();
            builder.RegisterType<TokenUserDetailsAuthenticationProvider>().As<IUserDetailsAuthenticationProvider>();
            builder.RegisterType<TokenAuthenticationFilter>().SingleInstance();
			builder.RegisterFilterProvider();

            builder.RegisterType<CachingBehavior>();
            builder.RegisterType<DefaultUserInfoService>()
                   .As<IUserInfoService>()
                   .InstancePerLifetimeScope()
                   .InterceptedBy(typeof(CachingBehavior))
                   .EnableInterfaceInterceptors();

            builder.RegisterType<LoggerInterceptor>();
            builder.RegisterType<DefaultLogger>()
               .As<ILogger>()
                   .InstancePerLifetimeScope()
                   .InterceptedBy(typeof(CachingBehavior))
                   .EnableInterfaceInterceptors();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
    }
}