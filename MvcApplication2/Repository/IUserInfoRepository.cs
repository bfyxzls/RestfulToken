using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.Repository
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public interface IUserInfoRepository
    {
        UserInfo GetUserInfoByName(string name);
        UserInfo ValidateByNamePasswword(string name,string password);
        List<UserInfo> GetUserInfos();
    }
}