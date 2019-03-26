using System;

namespace Lind.DDD.Caching
{
	/// <summary>
	/// 表示由此特性所描述的方法，能够获得来自Microsoft.Practices.EnterpriseLibrary.Caching基础结构层所提供的缓存功能。
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class CachingAttribute : Attribute
	{
		#region Ctor
		/// <summary>
		/// 初始化一个新的<c>CachingAttribute</c>类型。
		/// </summary>
		/// <param name="method">缓存方式。</param>
		public CachingAttribute(CachingMethod method)
		{
			this.Method = method;
		}
		/// <summary>
		/// 初始化一个新的<c>CachingAttribute</c>类型。
		/// </summary>
		/// <param name="method">缓存方式。</param>
		/// <param name="value">缓存的key</param>
		public CachingAttribute(CachingMethod method, string value, bool isAll = false)
			: this(method)
		{
			this.value = value;
			this.isAll = isAll;
		}
		#endregion

		#region Public Properties
		/// <summary>e
		/// 获取或设置缓存方式
		/// </summary>
		public CachingMethod Method { get; set; }
		/// <summary>
		/// 获取或设置与当前缓存方式相关的方法名称。注：此参数仅在缓存方式为Remove时起作用。
		/// </summary>
		public string value { get; set; }
		/// <summary>
		/// 删除缓存时，是否更新所有值
		/// </summary>
		/// <value><c>true</c> if is all; otherwise, <c>false</c>.</value>
		public bool isAll { get; set; }
		#endregion
	}
}
