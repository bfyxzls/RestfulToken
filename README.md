# RestfulToken项目介绍
主要是一种设计理念，面向抽象，面向组件化的设计，我们的项目可能越来越多，但他们这间不直接依赖具体的实现，只依赖于抽象的接口。
### 共用组件项目
* Lind.Authorization 授权项目
* 

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