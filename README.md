# RestfulToken项目介绍
主要是一种设计理念，面向抽象，面向组件化的设计，我们的项目可能越来越多，但他们这间不直接依赖具体的实现，只依赖于抽象的接口。
### 共用组件项目
* Lind.Authorization 授权项目
* Lind.DI 类似于Spring Ioc风格的依赖注入项目

### Lind.Authorization注入方法
在自己的项目中实现对应的接口,`UserInfo`,`UserService`,`TokenUserDetailsAuthenticationProvider`都是根据自己项目的业务去实现的。
```
 //注入Lind.Authorization依赖关系，在自己项目里实现对应的方法
 builder.RegisterType<UserInfo>().As<IUserDetails>();
 builder.RegisterType<UserService>().As<IUserDetailsService>();
 builder.RegisterType<TokenUserDetailsAuthenticationProvider>().As<IUserDetailsAuthenticationProvider>();
 builder.RegisterType<TokenAuthenticationFilter>().SingleInstance();
 builder.RegisterFilterProvider();
 //注入全局过程器
 filters.Add(DependencyResolver.Current.GetService<Lind.Authorization.TokenAuthenticationFilter>());

```
### Lind.DI
事实上，Lind.DI是所有组件的最底层依赖，它实现了对象的注册和注入，类于spring ioc的简约风格更适合当代开发人员，大叔的Lind.DI是以autofac这个ioc容器为基础的，在它上面进行了封装，这也就是拿来主义的思想吧。
* 统一的注入方式 
```
public class DIFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Lind.DI.DIFactory.InjectFromObject(filterContext.Controller);
		}
	}
```
* 统一的注册组件
```
Lind.DI.DIFactory.Init();
```
* 定义组件
```
	[Lind.DI.Component]
	public class ProductService:IProductService
 {
		public string getProductName()
		{
			return "ioc for product service.";
		}
	}
```
* 注入和使用组件
```
[Lind.DI.Injection]
IProductService productService;
public ActionResult DI()
{
			return Content(productService.getProductName());	
}
```
