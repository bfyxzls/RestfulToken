using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lind.DDD.Caching;
using MvcApplication2.DITest;
using MvcApplication2.Service;

namespace MvcApplication2.Controllers
{
	[System.Web.Mvc.AllowAnonymousAttribute]
	public class TestController : Controller
	{
		IUserInfoService userInfoService;
		ILogger logger;
		public TestController(IUserInfoService userInfoService,ILogger logger)
		{
			this.userInfoService = userInfoService;
			this.logger = logger;
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

		public DateTime GetTime()
		{
            logger.WriteLine("hello");

			return userInfoService.GetTime();
		}

		[Caching(CachingMethod.Put,"TestController.GetTime")]
		public String SetTime(){
			return "Success";
		}
	}
}
