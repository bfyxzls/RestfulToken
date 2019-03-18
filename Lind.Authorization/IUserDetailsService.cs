using System;
namespace Lind.Authorization
{
	/// <summary>
    /// 授权对象获取接口
    /// </summary>
	public interface IUserDetailsService
	{
        /// <summary>
        /// 通过名称获取用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
		IUserDetails LoadUserByUsername(string username);
	}
}
