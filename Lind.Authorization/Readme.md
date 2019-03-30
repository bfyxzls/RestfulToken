### Lind.Authorization概述
Lind.Authorization是一个授权架构体系，主不但有授权的核心逻辑，而且也是面向接口的体现，授权的核心逻辑是固定的，`TokenAuthenticationFilter`是一种业务场景的功能组件，它的主逻辑不能修改，但里面的每块内容可以根据项目自身去实现，这类型于模板方法模式，它规定的业务流程，开发人员根据具体业务去实现里面的细节。

### Lind.Authorization组成
* IUserDetails授权实体接口，可能是用户表，账户表等
* IUserDetailsService授权实体业务接口，规定了授权时需要的方法，具体项目需要去实现它
* IUserDetailsAuthenticationProvider授权提供者接口，实现了基本的授权业务代码，具体项目可以覆盖它的方法
* TokenAuthenticationFilter基于token的授权过滤器，主要实现了对请求方法的拦截，它是授权的入口
* TokenUserDetailsAuthenticationProvider为token过滤器实现的授权管理者，提供一些公开的方法，使用者可以继承它，根据自己需要重写里面的方法

### TokenAuthenticationFilter认证的过程
下面看一下授权组件的依赖关系：
```
TokenAuthentictokenationFilter
>
IUserDetailsAuthenticationProvider
>
IUserDetailsService
>
IUserDetails
```
开发人员如果希望在自己项目中使用它，最少要实现这种个接口
```
IUserDetailsService：用户获取，token生成，token获取
IUserDetails：用户实体

```