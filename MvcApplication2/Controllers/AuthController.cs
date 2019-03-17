using MvcApplication2.Models;
using MvcApplication2.Repository;
using MvcApplication2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Caching;
using System.Web.Http;

namespace MvcApplication2.Controllers
{
    /// <summary>
    /// 授权控制器，AllowAnonymousAttribute表示可以匿名访问，不需要token
    /// </summary>
    [AllowAnonymousAttribute]
    public class AuthController : ApiController
    {
        IUserInfoRepository userInfoRepository = new UserInfoRepository();
        ICache cache = new RuntimeCache();

        [HttpGet]
        public string Login(string username, string password)
        {
            UserInfo user = userInfoRepository.ValidateByNamePasswword(username, password);
            #region 生成token并返回
            string token = Guid.NewGuid().ToString();
            cache.Put(token, user.Username, 24 * 60);//超时1天
            #endregion
            return token;
        }


    }
}
