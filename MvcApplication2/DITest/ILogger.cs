using System;
using Autofac.Extras.DynamicProxy2;

namespace MvcApplication2.DITest
{
	public interface ILogger
	{
		void WriteLine(String line);
	}
	public class DefaultLogger : ILogger
	{
		public void WriteLine(string line)
		{
			Console.WriteLine("Default Logger : {0}", line);
		}
	}

}
