using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Lind.DDD.Caching
{
	/// <summary>
	/// 表示用于方法缓存功能的拦截行为。
	/// </summary>
	public class CachingBehavior : IInterceptor
	{
		/// <summary>
		/// 缓存项目名称，每个项目有自己的名称
		/// 避免缓存键名重复
		/// </summary>
		static readonly string cacheProjectName = ConfigurationManager.AppSettings["CacheProjectName"] ?? "DataSetCache";


		#region IInterceptionBehavior Members

		/// <summary>
		/// 通过实现此方法来拦截调用并执行所需的拦截行为.
		/// </summary>
		/// <param name="input">input.</param>
		public void Intercept(IInvocation input)
		{
			Console.WriteLine("Caching Intercept");
			var method = input.Method;
			//键值前缀
			string key = cacheProjectName + "_" + method.DeclaringType.Name;

			if (method.IsDefined(typeof(CachingAttribute), false))
			{
				var cachingAttribute = (CachingAttribute)method.GetCustomAttributes(typeof(CachingAttribute), false)[0];
				string valKey = cachingAttribute.value;
				switch (cachingAttribute.Method)
				{
					case CachingMethod.Get:
						if (CacheManager.Instance.Exists(key, valKey))
						{
							var obj = CacheManager.Instance.Get(key, valKey);
							input.ReturnValue = obj;
						}
						else
						{
							input.Proceed();
							CacheManager.Instance.Add(key, valKey, input.ReturnValue);

						}
						break;

					case CachingMethod.Put:

						input.Proceed();
						CacheManager.Instance.Put(key, valKey, input.ReturnValue);
						break;

					case CachingMethod.Remove:

						CacheManager.Instance.Remove(key, valKey);
						break;

				}
			}

		}
              
		#endregion
	}
}
