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
		static readonly string splitStr = "_";
		/// <summary>
		/// 获取与某一特定参数值相关的键名。
		/// </summary>
		private string GetValueKey(CachingAttribute cachingAttribute, IInvocation input)
		{
			object[] inputArguments = input.Arguments;
			ParameterInfo[] parameters = input.Method.GetParameters();
			if (inputArguments != null && inputArguments.Length > 0)
			{
				var sb = new StringBuilder();
				for (int i = 0; i < input.Arguments.Length; i++)
				{
					if (input.Arguments[i] == null)
						break;

					if (input.Arguments[i].GetType().BaseType == typeof(LambdaExpression))//lambda处理
					{
						var result = new StringBuilder();

						try
						{
							var exp = inputArguments[i] as LambdaExpression;
							var arr = ((System.Runtime.CompilerServices.Closure)(((System.Delegate)(Expression.Lambda(exp).Compile().DynamicInvoke())).Target)).Constants;
							Type t = arr[0].GetType();
							result.Append(parameters[i].Name).Append(splitStr);
							foreach (var member in t.GetFields())
							{
								result.Append(member.Name)
									  .Append(splitStr)
									  .Append(t.GetField(member.Name).GetValue(arr[0]))
									  .Append(splitStr);
							}
							result = result.Remove(0, 1);
						}
						catch (NullReferenceException)
						{
							//lambda表达式异常，可能是没有字段，如这种格式i=>true，会产生NullReferenceException异常.
						}

						sb.Append(result.ToString());
					}
					else if (inputArguments[i].GetType() != typeof(string)//类和结构体处理
							 && inputArguments[i].GetType().IsClass)
					{
						var obj = input.Arguments[i];
						Type t = obj.GetType();
						var result = new StringBuilder();
						#region 提取类中的字段和属性
						result.Append(parameters[i].Name).Append(splitStr);
						foreach (var member in t.GetProperties())//公开属性
						{
							result.Append(member.Name)
								  .Append(splitStr)
								  .Append(t.GetProperty(member.Name).GetValue(obj, null))
								  .Append(splitStr);
						}
						foreach (var member in t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))//私有和公用字段
						{
							result.Append(member.Name)
								  .Append(splitStr)
								  .Append(t.GetField(member.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj))
								  .Append(splitStr);
						}
						#endregion
						sb.Append(result.ToString().Remove(result.Length - 1));
					}
					else//简单值类型处理
					{
						sb.Append(parameters[i].Name + splitStr + inputArguments[i].ToString());
					}

					if (i != inputArguments.Length - 1)
						sb.Append(splitStr);
				}
				return sb.ToString();
			}
			else
				return "";
		}

		#region IInterceptionBehavior Members

		/// <summary>
		/// 通过实现此方法来拦截调用并执行所需的拦截行为.
		/// </summary>
		/// <param name="input">input.</param>
		public void Intercept(IInvocation input)
		{
			var method = input.Method;
			//键值前缀
			string key = cacheProjectName + splitStr + method.DeclaringType.Name + splitStr;

			if (method.IsDefined(typeof(CachingAttribute), false))
			{
				var cachingAttribute = (CachingAttribute)method.GetCustomAttributes(typeof(CachingAttribute), false)[0];
				key = key + cachingAttribute.value;
				string valKey = GetValueKey(cachingAttribute, input);
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
