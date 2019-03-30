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
        /// <summary>
        /// 生成token并返回
        /// </summary>
        /// <param name="userDetails">用户对象</param>
        /// <returns></returns>
        string GenerateToken(IUserDetails userDetails);
        /// <summary>
        /// 校验token的有效性
        /// </summary>
        /// <param name="userDetails">用户对象</param>
        /// <returns></returns>
        void ValidateToken(string token);
	}
}
