using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lind.Authorization;
using Lind.DDD.Caching;
using MvcApplication2.DITest;
using MvcApplication2.Service;

namespace MvcApplication2.Controllers
{
	[AllowAttribute]
	public class TestController : Controller
	{
		[Lind.DI.Injection]
		IProductService productService;
		IUserInfoService userInfoService;
		ILogger logger;
		public TestController(IUserInfoService userInfoService, ILogger logger)
		{
			this.userInfoService = userInfoService;
			this.logger = logger;
		}
		public ActionResult DI()
		{
			return Content(productService.getProductName());	
		}
		//
		// GET: /Test/
		public ActionResult Index(int? id)
		{
			if (!id.HasValue)
			{
				throw new Exception("需要提供id参数");
			}
			return Content("success");
		}

		/// <summary>
		/// 获到数据，然后添加到缓存
		/// </summary>
		/// <returns>The time.</returns>
		public DateTime GetTime()
		{
			return userInfoService.GetTime();
		}

		/// <summary>
		/// 获到数据，然后添加到缓存
		/// </summary>
		/// <returns>The time by identifier.</returns>
		public DateTime GetTimeById()
		{
			return userInfoService.GetTime(1);
		}

		/// <summary>
		/// 让GetTime里的缓存失效
		/// </summary>
		/// <returns>The time.</returns>
		public String SetTime()
		{
			userInfoService.SetTime();
			return "Success";
		}

		/// <summary>
		/// 让GetTimeById里的缓存失效
		/// </summary>
		/// <returns>The time by identifier.</returns>
		public String SetTimeById()
		{
			userInfoService.SetTimeById(1, "ok");
			return "Success";
		}
	}
}
