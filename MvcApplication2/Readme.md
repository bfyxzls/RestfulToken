### token授权
用户api不能直接访问，需要使用方提代正确的token，而token由用户登陆时生成，然后分发给使用者，使用者在访问其它资源时，
在http请求头或者http请求参数时带上token.
#### 登陆api
```
/api/auth
```
#### 用户资源api
```
/api/user
```
没有登陆，直接访问用户资源会有如下的错误提示
```
{"Status":0,"ErrorCode":"401","ErrorMessage":"not find token"}
```
这时，
```
/auth/login?username=zzl&password=123
```
这时，登录成功后，返回授权的token码，如下
```
<string>c947addf-b343-497e-84a8-6dc6c1352155</string>
```
我们再去访问用户资源，这时我们把toke带上，你可以用postman这些工具 去测试 ，也可以 直接 在浏览器上测试 
```
/user/getuser?Authorization=Bearer c947addf-b343-497e-84a8-6dc6c1352155
``
注意Bearer和token之间有个空格，这是一种规范，我们 应该遵守。
再看一下测试的结果
```
<ArrayOfUserInfo><UserInfo><Id>1</Id><Password>123</Password><Username>zzl</Username></UserInfo></ArrayOfUserInfo>
```