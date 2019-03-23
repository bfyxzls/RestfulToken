using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        [System.Web.Mvc.AllowAnonymousAttribute]
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("需要提供id参数");
            }
            return Content("success");
        }

    }
}
