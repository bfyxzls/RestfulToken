using Autofac.Extras.DynamicProxy2;
using Lind.DDD.Caching;
using MvcApplication2.Models;
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
		[Caching(CachingMethod.Get, value = "time")]
		DateTime GetTime();
		[Caching(CachingMethod.Remove, value = "time")]
		void SetTime();
	}
}