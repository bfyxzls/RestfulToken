using MvcApplication2.Models;
using MvcApplication2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication2.Controllers
{
    /// <summary>
    /// 用户中心，需要有token才能访问
    /// </summary>
    public class UserController : ApiController
    {
        IUserInfoRepository userInfoRepository = new UserInfoRepository();
        [HttpGet]
        public List<UserInfo> getUser()
        {
            return userInfoRepository.GetUserInfos();
        }
    }
}
