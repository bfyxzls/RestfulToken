using Lind.Authorization;
using MvcApplication2.Models;
using MvcApplication2.Service;
using MvcApplication2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    /// <summary>
    /// 授权控制器，AllowAnonymousAttribute表示可以匿名访问，不需要token
    /// </summary>
    public class AuthController : Controller
    {
        [Lind.DI.Injection]
        IUserInfoService userInfoRepository;
        [Lind.DI.Injection]
        IUserDetailsService userDetailsService;


        [AllowAttribute]
        public string Login(string username, string password)
        {
            UserInfo user = userInfoRepository.ValidateByNamePasswword(username, password);
            #region 生成token并返回
            return userDetailsService.GenerateToken(user);
            #endregion

        }

    }
}
