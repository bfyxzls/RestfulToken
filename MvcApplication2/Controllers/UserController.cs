using MvcApplication2.Models;
using MvcApplication2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
	/// <summary>
	/// 用户中心，需要有token才能访问
	/// </summary>
	public class UserController : Controller
	{
		IUserInfoService userInfoRepository;
		public UserController(IUserInfoService userInfoRepository)
		{
			this.userInfoRepository = userInfoRepository;
		}
	
		public List<UserInfo> getUser()
		{
			return userInfoRepository.GetUserInfos();
		}
	}
}
