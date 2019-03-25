using System;
using Castle.DynamicProxy;

namespace MvcApplication2.DITest
{
	public class LoggerInterceptor : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			Console.WriteLine("Interceptor method logger");
		}
	}
}
