using System;
namespace Lind.Authorization
{
	/// <summary>
    /// 允许访问的特性.
    /// </summary>
	[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class AllowAttribute : Attribute
	{
	}
}
