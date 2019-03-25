using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lind.DDD.Caching;
using MvcApplication2.Models;

namespace MvcApplication2.Service
{
	/// <summary>
	/// 仓储实现
	/// </summary>
	public class DefaultUserInfoService : IUserInfoService
	{
		static List<UserInfo> dbUserInfos = new List<UserInfo>();
		static DefaultUserInfoService()
		{
			dbUserInfos.Add(new UserInfo { Id = 1, Username = "zzl", Password = "123" });
		}

		public DateTime GetTime()
		{
			return dateTime;
		}

		public UserInfo GetUserInfoByName(string name)
		{
			UserInfo user = dbUserInfos.Find(i => i.Username.Equals(name));
			if (user == null)
				throw new HttpException("用户没有被找到");
			return user;
		}

		public List<UserInfo> GetUserInfos()
		{
			return dbUserInfos;
		}
		DateTime dateTime = DateTime.Now;
		public void SetTime()
		{
			//设置时间对象
			dateTime = DateTime.Now;
			Console.WriteLine("更新缓存");
		}

		public UserInfo ValidateByNamePasswword(string name, string password)
		{
			UserInfo user = dbUserInfos.Find(i => i.Username.Equals(name) && i.Password.Equals(password));
			if (user == null)
				throw new HttpException("用户名密码不匹配");
			return user;
		}
	}
}