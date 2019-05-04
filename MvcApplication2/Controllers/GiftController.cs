using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    /// <summary>
    /// 异步和并行
    /// </summary>
    public class GiftController : Controller
    {
        public async Task<ActionResult> Index()
        {
            // .net4.5的特性:并行:一个比较大的任务,由多个线程同时执行,这就是并行,线程之间是相互独立的,它们执行的结果由主线程从新组织,并返回.
            Step1();
            Step2();
            return Content("执行完成");
        }

        /// <summary>
        /// 任务的第一个步骤
        /// </summary>
        async void Step1()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Task Run 1");
                Thread.Sleep(2000);
                Console.WriteLine("Task Run 2");
            });
        }

        /// <summary>
        /// 任务的第二个步骤
        /// </summary>
        async void Step2()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Task Run 1");
                Thread.Sleep(3000);
                Console.WriteLine("Task Run 2");
            });
        }
    }
}
