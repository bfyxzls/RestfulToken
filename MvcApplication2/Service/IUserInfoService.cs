﻿using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.Service
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public interface IUserInfoService
    {
        UserInfo GetUserInfoByName(string name);
        UserInfo ValidateByNamePasswword(string name, string password);
        List<UserInfo> GetUserInfos();
		DateTime GetTime();
    }
}